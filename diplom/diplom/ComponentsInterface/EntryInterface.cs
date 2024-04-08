using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace diplom.ComponentsInterface
{
    public class EntryInterface
    {
        public void OnlyNumbers(Entry entry)
        {
            entry.TextChanged += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(entry.Text))
                {
                    if (!entry.Text.All(char.IsDigit))
                    {
                        entry.Text = new string(entry.Text.Where(char.IsDigit).ToArray());
                    }
                }
            };
        }
    }
}
