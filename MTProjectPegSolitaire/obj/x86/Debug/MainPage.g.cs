﻿#pragma checksum "C:\Users\pepe\Documents\New folder\MTProjectPegSolitaire\MTProjectPegSolitaire\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DFD6B70BCB9509717813BB3B95A8C087"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MTProjectPegSolitaire
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.MainGrid = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 2:
                {
                    this.ContinueButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 31 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.ContinueButton).Tapped += this.ContinueButton_Tapped;
                    #line default
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 32 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Tapped += this.Button_NewGame_Tapped;
                    #line default
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 33 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Tapped += this.Button_Tapped_2;
                    #line default
                }
                break;
            case 5:
                {
                    global::Windows.UI.Xaml.Controls.Button element5 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 34 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element5).Tapped += this.Button_Tapped;
                    #line default
                }
                break;
            case 6:
                {
                    this.EasyRB = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 26 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.EasyRB).Checked += this.RadioButton_Checked;
                    #line default
                }
                break;
            case 7:
                {
                    this.MediunRB = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 27 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.MediunRB).Checked += this.RadioButton_Checked;
                    #line default
                }
                break;
            case 8:
                {
                    this.HardRB = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                    #line 28 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.RadioButton)this.HardRB).Checked += this.RadioButton_Checked;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

