﻿#pragma checksum "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D40F3B1AE4E2FF2E5ACED4A33563B5CF"
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


namespace LEDX.Components.ColorPicker.ColorModels.HSB {
    
    
    /// <summary>
    /// HsbDisplay
    /// </summary>
    public partial class HsbDisplay : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rH;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtH;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rS;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtS;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rB;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtB;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Components;component/components/colorpicker/colormodels/hsb/hsbdisplay.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.rH = ((System.Windows.Controls.RadioButton)(target));
            
            #line 30 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
            this.rH.Checked += new System.Windows.RoutedEventHandler(this.rH_Checked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtH = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
            this.txtH.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txtR_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
            this.txtH.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.rS = ((System.Windows.Controls.RadioButton)(target));
            
            #line 35 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
            this.rS.Checked += new System.Windows.RoutedEventHandler(this.rS_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtS = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
            this.txtS.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.rB = ((System.Windows.Controls.RadioButton)(target));
            
            #line 39 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
            this.rB.Checked += new System.Windows.RoutedEventHandler(this.rB_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtB = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\HSB\HsbDisplay.xaml"
            this.txtB.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

