using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace StatusMonitorWPF
{
   public static class CustomAnimation
    {

        public static async Task Switch(Rectangle element, Func<bool> function)
        {
            while (true)
            {
                if (!function())
                {

                    element.Visibility = Visibility.Hidden;
                    await Task.Delay(700);
                    element.Visibility = Visibility.Visible;
                }
                else
                {
                    element.Visibility = Visibility.Hidden;
                }
                await Task.Delay(700);

            }

        }
        public static async Task SetData(System.Windows.Controls.Label info, Func<string> source)
        {
            while (true)
            {
                info.Content = source();
                await Task.Delay(500);
            }

        }
        public static async Task SetBarStaticGeneric(List<Rectangle> bars, Func<int> function, System.Windows.Controls.Label info, Func<bool> growing)
        {
            while (true)
            {
                int perc = function();
                int amount;
                if (perc != 0)
                {
                    amount = perc * bars.Count / 100;
                }
                else
                {
                    amount = 1;
                    perc = 1;
                }
                if (info != null)
                    info.Content = perc + "%";
                for (int i = 0; i < bars.Count; i++)
                {
                    if (i > amount)
                    {
                        bars[i].Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        bars[i].Visibility = Visibility.Visible;
                    }

                }
                if (growing())
                {
                    for (int i = amount + 1; i < bars.Count; i++)
                    {
                        if (!growing())
                        {
                            break;
                        }
                        bars[i].Visibility = Visibility.Visible;
                        await Task.Delay(300);
                    }
                    for (int i = amount + 1; i < bars.Count; i++)
                    {
                        bars[i].Visibility = Visibility.Hidden;
                    }
                }

                await Task.Delay(700);
            }


        }
    }
}
