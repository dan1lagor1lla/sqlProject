﻿#pragma checksum "..\..\..\TeacherWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7A4FE3B0023BAE0EED8CD9DDC6F87C8C595F0E26"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using sqlProject;


namespace sqlProject {
    
    
    /// <summary>
    /// TeacherWindow
    /// </summary>
    public partial class TeacherWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 176 "..\..\..\TeacherWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListOfTests;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\..\TeacherWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel TestSettingsViewer;
        
        #line default
        #line hidden
        
        
        #line 193 "..\..\..\TeacherWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton QuestionRandomnessToggleButton;
        
        #line default
        #line hidden
        
        
        #line 207 "..\..\..\TeacherWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListOfQuestions;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/sqlProject;component/teacherwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\TeacherWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 71 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.ContextMenu)(target)).Opened += new System.Windows.RoutedEventHandler(this.ListOfTestsContextMenuOpened);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 74 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTest);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 77 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveSelectedTests);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 80 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.ContextMenu)(target)).Opened += new System.Windows.RoutedEventHandler(this.ListOfQuestionsContextMenuOpened);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 83 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddQuestion);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 86 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveSelectedQuestions);
            
            #line default
            #line hidden
            return;
            case 14:
            this.ListOfTests = ((System.Windows.Controls.ListBox)(target));
            
            #line 177 "..\..\..\TeacherWindow.xaml"
            this.ListOfTests.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TestSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 15:
            this.TestSettingsViewer = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 16:
            this.QuestionRandomnessToggleButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 17:
            this.ListOfQuestions = ((System.Windows.Controls.ListBox)(target));
            return;
            case 18:
            
            #line 230 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseTeacherWindow);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 7:
            
            #line 95 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.UpdateSourceOfTextProperty);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 101 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveAnswer);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 108 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.Expander)(target)).Expanded += new System.Windows.RoutedEventHandler(this.ListOfAnswersExpanded);
            
            #line default
            #line hidden
            break;
            case 10:
            
            #line 115 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.UpdateSourceOfTextProperty);
            
            #line default
            #line hidden
            break;
            case 11:
            
            #line 126 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.ListBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SelectionChanged);
            
            #line default
            #line hidden
            break;
            case 12:
            
            #line 131 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddAnswer);
            
            #line default
            #line hidden
            break;
            case 13:
            
            #line 143 "..\..\..\TeacherWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.UpdateSourceOfTextProperty);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

