﻿using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class MainView
    {
        private readonly MainViewModel _viewModel =
            ServiceLocator.ViewModelLocator.MainViewModel;

        private const int offsetKnob = 4;
        private bool firstAttempt = true;

        public MainView()
        {
            InitializeComponent();

            // Set the background color of the status bar, and DON'T FORGET to set the opacity!
            //var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            //statusBar.BackgroundColor = Windows.UI.Colors.Red;
            //statusBar.BackgroundOpacity = 1;
            //statusBar.ProgressIndicator.Text = "My cool app";
            //statusBar.ProgressIndicator.ShowAsync();
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            while (ServiceLocator.NavigationService.CanGoBack)
                ServiceLocator.NavigationService.RemoveBackEntry();

            _viewModel.ShowMessagesCommand.Execute(null);
            base.OnNavigatedTo(e);
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
                if (bindingExpression != null)
                    bindingExpression.UpdateSource();
            }
        }

        private void FilterTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                _viewModel.LoadOnlineProjects(false);
            }
        }

        private void Hub_SectionsInViewChanged(object sender, SectionsInViewChangedEventArgs e)
        {
            if ((MainHub.SectionsInView[0] == HubSectionOnlineProjects) && firstAttempt)
            {
                _viewModel.LoadOnlineProjects(false, true);
                firstAttempt = false;
            }
        }

        private void ListViewOnlineProjects_ItemClick(object sender, ItemClickEventArgs e)
        {
            _viewModel.OnlineProjectTapCommand.Execute(e.ClickedItem);
        }


        //private void LocalProjectControl_OnLocalProjectsBackPressed(object sender, EventArgs e)
        //{
        //    SlideLeft(PanoramaMain);
        //}

        //protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //    base.OnNavigatingFrom(e);
        //}
    }
}
