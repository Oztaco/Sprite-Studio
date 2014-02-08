using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpriteStudio.CommonControls
{
    /// <summary>
    /// Interaction logic for NumberTextBox.xaml
    /// </summary>
    public partial class NumberTextBox : UserControl
    {
        public NumberTextBox()
        {
            InitializeComponent();
        }
        public void ValidateTextBox()
        {
            int selStart, selLength;
            selStart = txtBoxDataInput.SelectionStart;
            selLength = txtBoxDataInput.SelectionLength;
            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '-'};
            List<char> textArray = txtBoxDataInput.Text.ToCharArray().ToList<char>();
            bool matches = false;

            if (!_allowDecimals) numbers[10] = '-';

            for (int i = textArray.Count - 1; i >= 0; i--)
            {
                matches = false;
                for (int a = 0; a < numbers.Length; a++)
                {
                    if (textArray[i] == numbers[a])
                    {
                        matches = true;
                        break;
                    }
                }
                if (!matches)
                {
                    textArray.RemoveAt(i);
                }
            }
            txtBoxDataInput.Text = new string(textArray.ToArray());

            if (txtBoxDataInput.Text.Length == 0) { }
            else
            {
                if (selStart >= txtBoxDataInput.Text.Length) selStart = txtBoxDataInput.Text.Length;
                if (selStart + selLength >= txtBoxDataInput.Text.Length) selLength = 0;
                txtBoxDataInput.Select(selStart, selLength);
            }
        }

        private void txtBoxDataInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBox();
        }


        private bool allowDecimals = true;
        public bool _allowDecimals
        {
            get { return allowDecimals; }
            set
            {
                allowDecimals = value;
                ValidateTextBox();
            }
        }
        public float Value {
            get {
        		if (txtBoxDataInput.Text != "") 
        		return float.Parse(txtBoxDataInput.Text);
        		else if (0 < _minValue) return _minValue;
        		else return 0;
            }
            set {
                txtBoxDataInput.Text = value.ToString();
                ValueSet();
            }
        }
        private float _maxValue = float.PositiveInfinity;
        public float maxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
            	if (value > absMax) _maxValue = absMax;
                else _maxValue = value;
                
                if (Value > _maxValue) Value = _maxValue;
            }
        }
        private float _minValue = float.NegativeInfinity;
        public float minValue
        {
            get
            {
                return _minValue;
            }
            set
            {
            	if (value < -1 * absMax) _minValue = -1 * absMax;
                else _minValue = value;
                
                if (Value < _minValue) Value = _minValue;
            }
        }
        private float absMax = 9999999; // Max of the max

        public delegate void ValueSetDelegate();
        public ValueSetDelegate ValueSet = delegate { };

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        	if (Value != absMax) {
            float num = Value + 1;
            if (num <= maxValue) Value = num;
        	}
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        	if (Value != -1 * absMax) {
            float num = Value - 1;
            if (num >= minValue) Value = num;
        	}
        }
    }
}
