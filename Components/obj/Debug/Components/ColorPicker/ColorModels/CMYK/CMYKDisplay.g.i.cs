﻿#pragma checksum "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9814851C5031B2FEAD6A0FBF6E3429E3"
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


namespace LEDX.Components.ColorPicker.ColorModels.CMYK {
    
    
    /// <summary>
    /// CMYKDisplay
    /// </summary>
    public partial class CMYKDisplay : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtC;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtCUnit;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtM;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtMUnit;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtY;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtYUnit;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtK;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtKUnit;
        
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
            System.Uri resourceLocater = new System.Uri("/Components;component/components/colorpicker/colormodels/cmyk/cmykdisplay.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
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
            this.txtC = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
            this.txtC.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txtR_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
            this.txtC.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtCUnit = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.txtM = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
            this.txtM.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtMUnit = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.txtY = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
            this.txtY.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtYUnit = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.txtK = ((System.Windows.Controls.TextBox)(target));
            
            #line 45 "..\..\..\..\..\..\Components\ColorPicker\ColorModels\CMYK\CMYKDisplay.xaml"
            this.txtK.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.txtKUnit = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
