﻿#pragma checksum "F:\QLSinhVien_UWP\QLSinhVien_UWP\Views\ScoreView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FB9B13DD5AAA6297ACCDB91C0ADDDAF1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLSinhVien_UWP.Views
{
    partial class ScoreView : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\ScoreView.xaml line 127
                {
                    this.DtgvScore = (global::Microsoft.Toolkit.Uwp.UI.Controls.DataGrid)(target);
                    ((global::Microsoft.Toolkit.Uwp.UI.Controls.DataGrid)this.DtgvScore).SelectionChanged += this.DtgvScore_SelectionChanged;
                }
                break;
            case 3: // Views\ScoreView.xaml line 112
                {
                    this.DtgvStudent = (global::Microsoft.Toolkit.Uwp.UI.Controls.DataGrid)(target);
                    ((global::Microsoft.Toolkit.Uwp.UI.Controls.DataGrid)this.DtgvStudent).SelectionChanged += this.DtgvStudent_SelectionChanged;
                }
                break;
            case 4: // Views\ScoreView.xaml line 103
                {
                    this.TxbFindByID = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.TxbFindByID).TextChanged += this.TxbFindByID_TextChanged;
                }
                break;
            case 5: // Views\ScoreView.xaml line 104
                {
                    this.TxbFindByName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.TxbFindByName).TextChanged += this.TxbFindByID_TextChanged;
                }
                break;
            case 6: // Views\ScoreView.xaml line 105
                {
                    this.CbbClassType = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.CbbClassType).SelectionChanged += this.CbbClassType_SelectionChanged;
                }
                break;
            case 7: // Views\ScoreView.xaml line 107
                {
                    this.CbbClass = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.CbbClass).SelectionChanged += this.CbbClass_SelectionChanged;
                }
                break;
            case 8: // Views\ScoreView.xaml line 33
                {
                    this.SpTxbCourse = (global::Windows.UI.Xaml.Controls.VariableSizedWrapGrid)(target);
                }
                break;
            case 9: // Views\ScoreView.xaml line 47
                {
                    this.TxbTest = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.TxbTest).TextChanged += this.ValidTextbox_TextChanged;
                }
                break;
            case 10: // Views\ScoreView.xaml line 48
                {
                    this.TxbMidTerm = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.TxbMidTerm).TextChanged += this.ValidTextbox_TextChanged;
                }
                break;
            case 11: // Views\ScoreView.xaml line 49
                {
                    this.TxbFinal = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.TxbFinal).TextChanged += this.ValidTextbox_TextChanged;
                }
                break;
            case 12: // Views\ScoreView.xaml line 50
                {
                    this.TxbAverage = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 13: // Views\ScoreView.xaml line 51
                {
                    this.CbbCourse = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 14: // Views\ScoreView.xaml line 54
                {
                    this.SpButton = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 15: // Views\ScoreView.xaml line 56
                {
                    this.BtnAdd = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnAdd).Click += this.AddScore_Click;
                }
                break;
            case 16: // Views\ScoreView.xaml line 63
                {
                    this.BtnEdit = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnEdit).Click += this.BtnEdit_Click;
                }
                break;
            case 17: // Views\ScoreView.xaml line 70
                {
                    this.BtnSave = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnSave).Click += this.BtnSave_Click;
                }
                break;
            case 18: // Views\ScoreView.xaml line 77
                {
                    this.BtnSkip = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnSkip).Click += this.BtnSkip_Click;
                }
                break;
            case 19: // Views\ScoreView.xaml line 24
                {
                    this.TxbStudentID = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 20: // Views\ScoreView.xaml line 25
                {
                    this.TxbStudentName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 21: // Views\ScoreView.xaml line 26
                {
                    this.TxbClassName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 22: // Views\ScoreView.xaml line 27
                {
                    this.TxbBirthDate = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 23: // Views\ScoreView.xaml line 28
                {
                    this.TxbGender = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 24: // Views\ScoreView.xaml line 29
                {
                    this.TxbEmail = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 25: // Views\ScoreView.xaml line 30
                {
                    this.TxbAddress = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
