using System.Windows;
using System.Windows.Controls;
using TemplateSelectorDemo.ViewModel;

namespace TemplateSelectorDemo.Selector
{
   public class DemoTemplateSelector:DataTemplateSelector
    {
        public DataTemplate TemplateA { get; set; }
        public DataTemplate TemplateB { get; set; }        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (container is FrameworkElement && item != null)
            {
                if (item is AViewModel)
                    return TemplateA;

                if (item is BViewModel)
                    return TemplateB;
                else return null;
            }
            return null;
        }
    }
}
