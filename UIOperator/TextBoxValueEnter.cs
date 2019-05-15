 private bool textChangeFlag = false;

        private string textPre = "";

        private void TextBox_Joint_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textPre = textBox.Text;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textChangeFlag = true;
        }

        private void TextBox_Enter_Uint(object sender)
        {
            var textBox = sender as TextBox;
            float value = 0;
            bool isNumber = float.TryParse(textBox.Text, out value);
            if (isNumber && UIOperator.IsValid(textBox.Text, 0, 99))
            {
                textBox.Text = Convert.ToString(Convert.ToUInt16(Math.Abs(value)));
            }
            else
                textBox.Text = Convert.ToString(0);
        }

        private void TextBox_Enter_Ufloat(object sender)
        {
            var textBox = sender as TextBox;
            float value = 0;
            bool isNumber = float.TryParse(textBox.Text, out value);
            if (isNumber)
            {
                textBox.Text = Math.Abs(value).ToString("F");
            }
            else
                textBox.Text = Convert.ToString(0);
        }

        private void TextBox_Enter_Float(object sender, string stringFormat)
        {
            var textBox = sender as TextBox;
            float value = 0;
            bool isNumber = float.TryParse(textBox.Text, out value);
            if (isNumber)
            {
                textBox.Text = value.ToString(stringFormat);
            }
            else
                textBox.Text = Convert.ToString(0);
        }

        private void TextBox_Enter_Int(object sender)
        {
            var textBox = sender as TextBox;
            float value = 0;
            bool isNumber = float.TryParse(textBox.Text, out value);
            if (isNumber)
            {
                textBox.Text = Convert.ToString(Convert.ToInt16(value));
            }
            else
                textBox.Text = Convert.ToString(0);
        }

        private void TextBox_Enter_Byte(object sender)
        {
            var textBox = sender as TextBox;
            try
            {
                byte value = Convert.ToByte(textBox.Text, 16);
            }
            catch
            {
                textBox.Text = Convert.ToString(0);
            }
        }

        private void TextBox_LostFocus_Byte(object sender, RoutedEventArgs e)
        {
            if (textChangeFlag)
            {
                TextBox_Enter_Byte(sender);
                textChangeFlag = false;
            }
        }

        private void TextBox_KeyDown_Byte(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox_LostFocus_Byte(sender, e);
            }
        }

        private void TextBox_LostFocus_Int(object sender, RoutedEventArgs e)
        {
            if (textChangeFlag)
            {
                TextBox_Enter_Int(sender);
                textChangeFlag = false;
            }
        }

        private void TextBox_KeyDown_Int(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox_LostFocus_Int(sender, e);
            }
        }

        private void TextBox_LostFocus_Uint(object sender, RoutedEventArgs e)
        {
            if (textChangeFlag)
            {
                TextBox_Enter_Uint(sender);
                textChangeFlag = false;
            }
        }

        private void TextBox_KeyDown_Uint(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox_LostFocus_Uint(sender, e);
            }
        }

        private void TextBox_LostFocus_Float(object sender, RoutedEventArgs e)
        {
            if (textChangeFlag)
            {
                TextBox_Enter_Float(sender, "F");
                textChangeFlag = false;
            }
        }

        private void TextBox_KeyDown_Float(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox_LostFocus_Float(sender, e);
            }
        }

        private void TextBox_LostFocus_Float_3Digits(object sender, RoutedEventArgs e)
        {
            if (textChangeFlag)
            {
                TextBox_Enter_Float(sender, "N3");
                textChangeFlag = false;
            }
        }

        private void TextBox_KeyDown_Float_3Digits(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox_LostFocus_Float_3Digits(sender, e);
            }
        }

        private void TextBox_LostFocus_Ufloat(object sender, RoutedEventArgs e)
        {
            if (textChangeFlag)
            {
                TextBox_Enter_Ufloat(sender);
                textChangeFlag = false;
            }
        }

        private void TextBox_KeyDown_Ufloat(object sender, KeyEventArgs e)
        {
            if (textChangeFlag & e.Key == Key.Enter)
            {
                TextBox_LostFocus_Ufloat(sender, e);
            }
        }

        // The velocity should be setted between 0 to 100
        private void VelocityPreviewTextInputValue_TextChanged(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid((((TextBox)sender).Text + e.Text), 0, 100);
        }
        
