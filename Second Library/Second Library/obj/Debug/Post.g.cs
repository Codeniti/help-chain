﻿#pragma checksum "C:\Users\harshmasand\Documents\Visual Studio 2012\Projects\Second Library\Second Library\Post.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "47F3B053BE5DBD13379F61B7DC97BDE8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Second_Library {
    
    
    public partial class Post : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ProgressBar PB;
        
        internal System.Windows.Controls.TextBlock Notify;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock TextPost;
        
        internal System.Windows.Controls.TextBlock Views;
        
        internal System.Windows.Controls.TextBlock Respects;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Second%20Library;component/Post.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.PB = ((System.Windows.Controls.ProgressBar)(this.FindName("PB")));
            this.Notify = ((System.Windows.Controls.TextBlock)(this.FindName("Notify")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.TextPost = ((System.Windows.Controls.TextBlock)(this.FindName("TextPost")));
            this.Views = ((System.Windows.Controls.TextBlock)(this.FindName("Views")));
            this.Respects = ((System.Windows.Controls.TextBlock)(this.FindName("Respects")));
        }
    }
}

