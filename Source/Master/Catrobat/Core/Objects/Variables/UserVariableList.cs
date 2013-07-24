﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Variables
{
    public class UserVariableList : DataObject
    {
        public ObservableCollection<UserVariable> UserVariables;

        public UserVariableList() {UserVariables = new ObservableCollection<UserVariable>();}

        public UserVariableList(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            UserVariables = new ObservableCollection<UserVariable>();
            foreach (XElement element in xRoot.Elements())
            {
                UserVariables.Add(new UserVariable(element));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("list");

            foreach (UserVariable userVariable in UserVariables)
            {
                xRoot.Add(userVariable.CreateXML());
            }

            return xRoot;
        }

        public DataObject Copy()
        {
            var newUserVariableList = new UserVariableList();

            foreach (var userVariable in UserVariables)
                newUserVariableList.UserVariables.Add(userVariable.Copy() as UserVariable);

            return newUserVariableList;
        }
    }
}