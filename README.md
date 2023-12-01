# DataTemplateSelectorDemo
Demonstrates DataTemplateSelector
In Xaml we can use _type-driven_ views by mapping a __view__ to a ___data type___.
The UI is then able to insert the desired DataTemplate e.g. a _UserControl_ into the VisualTree 
of the current view based on the data-type it is bound to.

The tool that use to implement such behavior is the DataTemplateSelector
To help students understand how to employ DataTemplateSelector this project
demonstrates the basics. 

There are two parts in order to realize the _"data-awareness"_ in your application.

#Implement data templates in your main view
You need a FrameworkElement which can bind to a DependencyProperty and is able to host a content.
One such FrameworkElement is the __Label__.
The Label has an AttachedProperty "Content" which we can either 'hard-code' in XAML or
make use of the __ContentTemplateSelector__.
The latter needs a class, derrived from Template
```xaml
 <Label Content="{Binding CurrentViewModel}">
          <Label.ContentTemplateSelector>
              <sel:DemoTemplateSelector>
                  <sel:DemoTemplateSelector.TemplateA>
                      <DataTemplate>
                          <vw:AViewUserControl/>
                      </DataTemplate>
                  </sel:DemoTemplateSelector.TemplateA>
                  <sel:DemoTemplateSelector.TemplateB>
                      <DataTemplate>
                          <vw:BViewUserControl/>
                      </DataTemplate>
                  </sel:DemoTemplateSelector.TemplateB>
              </sel:DemoTemplateSelector>
          </Label.ContentTemplateSelector>
      </Label>
```csharp

