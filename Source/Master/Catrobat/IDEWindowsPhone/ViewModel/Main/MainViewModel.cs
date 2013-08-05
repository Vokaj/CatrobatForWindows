﻿using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using Catrobat.Core;
using Catrobat.Core.Misc;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.Content.Localization;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Themes;
using Catrobat.IDEWindowsPhone.Views.Main;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Media;
using System.Windows.Input;
using Catrobat.IDEWindowsPhone.Views.Service;
using Catrobat.IDEWindowsPhone.Views.Settings;
using Catrobat.IDEWindowsPhone.Views.Editor;

namespace Catrobat.IDEWindowsPhone.ViewModel.Main
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region private Members

        private readonly ObservableCollection<OnlineProjectHeader> _onlineProjects = new ObservableCollection<OnlineProjectHeader>();
        private string _filterText = "";
        private bool _isLoadingOnlineProjects;
        private MessageBoxResult _dialogResult;
        private string _deleteProductName;
        private string _copyProjectName;
        private Project _currentProject;
        private ImageSource _currentProjectScreenshot;

        #endregion

        #region Properties

        public CatrobatContextBase Context { get; set; }

        public Project CurrentProject
        {
            get
            {
                if (_currentProject == null)
                    return CurrentProject = Context.CurrentProject;

                return _currentProject;
            }
            set
            {
                if (value == _currentProject) return;

                _currentProject = value;

                RaisePropertyChanged(() => CurrentProject);

                var projectChangedMessage = new GenericMessage<Project>(Context.CurrentProject);
                Messenger.Default.Send<GenericMessage<Project>>(projectChangedMessage, ViewModelMessagingToken.SelectedProjectListener);
            }
        }

        public ProjectDummyHeader PinProjectHeader { get; set; }

        public ImageSource CurrentProjectScreenshot
        {
            get
            {
                return _currentProjectScreenshot;
            }

            set
            {
                if (_currentProjectScreenshot == value) return;

                _currentProjectScreenshot = value;
                RaisePropertyChanged(() => CurrentProjectScreenshot);
            }
        }

        public ObservableCollection<ProjectDummyHeader> LocalProjects { get { return Context.LocalProjects; } }

        public OnlineProjectHeader SelectedOnlineProject { get; set; }

        public ObservableCollection<OnlineProjectHeader> OnlineProjects
        {
            get
            {
                return _onlineProjects;
            }
        }

        public string ApplicationVersionName
        {
            get
            {
                var name =String.Format(AppResources.Main_ApplicationNameAndVersion,
                    PlatformInformationHelper.CurrentApplicationVersion);
                return name;
            }
        }

        public String FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    LoadOnlineProjects(false);
                    RaisePropertyChanged(() => FilterText);
                }
            }
        }

        public bool IsLoadingOnlineProjects
        {
            get { return _isLoadingOnlineProjects; }
            set { _isLoadingOnlineProjects = value; RaisePropertyChanged(() => IsLoadingOnlineProjects); }
        }

        public bool IsActiveatingLocalProject
        {
            get
            {
                return _isLoadingOnlineProjects;
            }
            set
            {
                _isLoadingOnlineProjects = value;
                RaisePropertyChanged(() => IsActiveatingLocalProject);
            }
        }

        #endregion

        #region Commands

        public ICommand RenameCurrentProjectCommand
        {
            get;
            private set;
        }

        public ICommand DeleteLocalProjectCommand
        {
            get;
            private set;
        }

        public ICommand CopyLocalProjectCommand
        {
            get;
            private set;
        }

        public ICommand PinLocalProjectCommand
        {
            get;
            private set;
        }

        public ICommand LazyLoadOnlineProjectsCommand
        {
            get;
            private set;
        }

        public ICommand SetCurrentProjectCommand
        {
            get;
            private set;
        }

        public ICommand CreateNewProjectCommand
        {
            get;
            private set;
        }

        public ICommand EditCurrentProjectCommand
        {
            get;
            private set;
        }

        public ICommand SettingsCommand
        {
            get;
            private set;
        }

        public ICommand OnlineProjectTapCommand
        {
            get;
            private set;
        }

        public ICommand PlayCurrentProjectCommand
        {
            get;
            private set;
        }

        public ICommand UploadCurrentProjectCommand
        {
            get;
            private set;
        }

        public RelayCommand ResetViewModelCommand
        {
            get;
            private set;
        }

        #endregion

        # region Actions

        private void RenameCurrentProjectAction()
        {
            var message = new GenericMessage<Project>(CurrentProject);
            Messenger.Default.Send<GenericMessage<Project>>(message, ViewModelMessagingToken.ProjectNameListener);

            Navigation.NavigateTo(typeof(ProjectSettingsView));
        }

        private void DeleteLocalProjectAction(string projectName)
        {
            _deleteProductName = projectName;

            var message = new DialogMessage(String.Format(AppResources.Main_MainDeleteProjectDialogMessage, projectName), DeleteProductMessageCallback)
            {
                Button = MessageBoxButton.OKCancel,
                Caption = AppResources.Main_MainDeleteProjectDialogTitle
            };

            Messenger.Default.Send(message);
        }

        private void CopyLocalProjectAction(string projectName)
        {
            _copyProjectName = projectName;

            var message = new DialogMessage(String.Format(AppResources.Main_MainCopyProjectDialogMessage, projectName), CopyProductMessageCallback)
            {
                Button = MessageBoxButton.OKCancel,
                Caption = AppResources.Main_MainCopyProjectDialogTitle
            };

            Messenger.Default.Send(message);
        }

        private void PinLocalProjectAction(ProjectDummyHeader project)
        {
            PinProjectHeader = project;
            Navigation.NavigateTo(typeof(TileGeneratorView));
        }

        private void LazyLoadOnlineProjectsAction()
        {
            LoadOnlineProjects(true);
        }

        private void SetCurrentProjectAction(string projectName)
        {
            var minLoadingTime = new TimeSpan(0, 0, 0, 0, 800);
            DateTime startTime = DateTime.UtcNow;
            IsActiveatingLocalProject = true;

            var setActiveTask = Task.Run(() =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    Context.SetCurrentProject(projectName);
                    var minWaitindTimeRemaining = minLoadingTime.Subtract(DateTime.UtcNow.Subtract(startTime));

                    if (minWaitindTimeRemaining >= new TimeSpan(0))
                        Thread.Sleep(minWaitindTimeRemaining);

                    IsActiveatingLocalProject = false;
                });

            });

        }

        private void CreateNewProjectAction()
        {
            Navigation.NavigateTo(typeof(AddNewProjectView));
        }

        private void EditCurrentProjectAction()
        {
            Navigation.NavigateTo(typeof(EditorLoadingView));
        }

        private void SettingsAction()
        {
            Navigation.NavigateTo(typeof(SettingsView));
        }

        private void OnlineProjectTapAction(OnlineProjectHeader project)
        {
            SelectedOnlineProject = project;
            Navigation.NavigateTo(typeof(OnlineProjectView));
        }

        private void PlayCurrentProjectAction()
        {
            PlayerLauncher.LaunchPlayer(CurrentProject.ProjectHeader.ProgramName);
        }

        private void UploadCurrentProjectAction()
        {
            Navigation.NavigateTo(typeof(UploadProjectsLoadingView));

            // Determine which page to open
            Task.Run(() => ServerCommunication.CheckToken(CatrobatContextBase.GetContext().CurrentToken, CheckTokenEvent));
        }

        private void ResetViewModelAction()
        {
            ResetViewModel();
        }

        #endregion


        public MainViewModel()
        {

            if (IsInDesignMode)
            {
                CatrobatContextBase.SetContextHolder(new ContextHolderDesign());
                Context = new CatrobatContextDesign();
            }
            else
            {
                RenameCurrentProjectCommand = new RelayCommand(RenameCurrentProjectAction);
                DeleteLocalProjectCommand = new RelayCommand<string>(DeleteLocalProjectAction);
                CopyLocalProjectCommand = new RelayCommand<string>(CopyLocalProjectAction);
                PinLocalProjectCommand = new RelayCommand<ProjectDummyHeader>(PinLocalProjectAction);
                LazyLoadOnlineProjectsCommand = new RelayCommand(LazyLoadOnlineProjectsAction);
                SetCurrentProjectCommand = new RelayCommand<string>(SetCurrentProjectAction);
                OnlineProjectTapCommand = new RelayCommand<OnlineProjectHeader>(OnlineProjectTapAction);
                SettingsCommand = new RelayCommand(SettingsAction);
                CreateNewProjectCommand = new RelayCommand(CreateNewProjectAction);
                EditCurrentProjectCommand = new RelayCommand(EditCurrentProjectAction);
                PlayCurrentProjectCommand = new RelayCommand(PlayCurrentProjectAction);
                UploadCurrentProjectCommand = new RelayCommand(UploadCurrentProjectAction);
                ResetViewModelCommand = new RelayCommand(ResetViewModelAction);

                Context = new CatrobatContext(); 
                Context.PropertyChanged += CatrobatContextPropertyChanged;
                Context.CurrentProject.PropertyChanged += CurrentProjectPropertyChanged;
            }



        }

        #region MessageBoxCallback

        private void LoadOnlineProjectsCallback(List<OnlineProjectHeader> projects, bool append)
        {
            if (!append)
            {
                _onlineProjects.Clear();
            }

            IsLoadingOnlineProjects = false;

            foreach (OnlineProjectHeader header in projects)
            {
                _onlineProjects.Add(header);
            }
        }

        private void DeleteProductMessageCallback(MessageBoxResult result)
        {
            _dialogResult = result;

            if (_dialogResult == MessageBoxResult.OK)
            {
                CatrobatContextBase.GetContext().DeleteProject(_deleteProductName);
                _deleteProductName = null;
            }
        }

        private void CopyProductMessageCallback(MessageBoxResult result)
        {

            _dialogResult = result;

            if (_dialogResult == MessageBoxResult.OK)
            {
                CatrobatContextBase.GetContext().CopyProject(_copyProjectName);
                _copyProjectName = null;
            }
        }

        #endregion


        public void LoadOnlineProjects(bool append)
        {
            if (!IsLoadingOnlineProjects)
            {
                IsLoadingOnlineProjects = true;
                ServerCommunication.LoadOnlineProjects(append, _filterText, _onlineProjects.Count, LoadOnlineProjectsCallback);
            }

        }

        private void CheckTokenEvent(bool registered)
        {
            if (registered)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    Navigation.NavigateTo(typeof(UploadProjectView));
                    Navigation.RemoveBackEntry();
                });
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    Navigation.NavigateTo(typeof(UploadProjectLoginView));
                    Navigation.RemoveBackEntry();
                });
            }
        }

        #region PropertyChanges

        public void CatrobatContextPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyNameHelper.GetPropertyNameFromExpression(() => CurrentProject))
            {
                CurrentProject = CatrobatContextBase.GetContext().CurrentProject;
                CurrentProjectScreenshot = CurrentProject.ProjectScreenshot as ImageSource;

                CatrobatContextBase.GetContext().CurrentProject.PropertyChanged += CurrentProjectPropertyChanged;
            }
        }

        public void CurrentProjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyNameHelper.GetPropertyNameFromExpression(() => CurrentProjectScreenshot))
                CurrentProjectScreenshot = CurrentProject.ProjectScreenshot as ImageSource;
        }

        #endregion

        private void ResetViewModel()
        {
        }
    }
}
