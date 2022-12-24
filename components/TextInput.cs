using launchspace_desktop.lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace launchspace_desktop.components
{
    internal class TextInput : TextBox
    {

        private static readonly SolidColorBrush PLACEHOLDER_COLOR = Constants.TERTIARY_COLOR;
        private static readonly SolidColorBrush TEXT_COLOR = Constants.TEXT_COLOR;
        private string placeholder = "";//placeholder text
        private bool isPlaceholder = false; //if the text in the box is currently placeholder

        public TextInput()
        {
            this.Background = Constants.SECONDARY_COLOR;
            this.Foreground = Constants.TEXT_COLOR;
            this.BorderThickness = new System.Windows.Thickness(1);
            this.BorderBrush = Constants.TERTIARY_HOVER_COLOR;
            this.FontWeight = FontWeights.Bold;
        }

        public void SetPlaceholder(string placeholder)
        {
            this.placeholder = placeholder;
            this.Foreground = PLACEHOLDER_COLOR;
            this.Text = placeholder;
            isPlaceholder = true;

        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            if (isPlaceholder)
            {
                this.Text = "";
                this.Foreground = TEXT_COLOR;
                this.isPlaceholder = false;

            }

        }

 

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            if (this.Text.Trim() == "")
            {
                isPlaceholder = true;
                this.Text = placeholder;
                this.Foreground = PLACEHOLDER_COLOR;
            }
         
        }


        

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if the input is empty</returns>
        public bool IsEmpty()
        {
            return isPlaceholder || this.Text.Trim() == "";
        }





    }
}
