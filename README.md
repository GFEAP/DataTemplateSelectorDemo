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
We provide a class derrived from DataTemplateSelector which decides based on the type of data what DataTemplate to use.


```csharp
    public override DataTemplate SelectTemplate(object item, DependencyObject container)

It's interface to the VisualTree is the method __SelectTemplate__; based on the data type supplied it returns a _DataTemplate_.

This DataTemplate is __not__ a view or UserControl! It does not posses a VisualTree, we provide that DataTemplate either 'inline' or
we use a UserControl in our project.

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

Where does the xmlns 'sel' come from?

In our example view _MainView_ we create an instance of our DemoDataSelector class by putting it in the window's resources.
```xaml
    <Window.Resources>
        <sel:DemoTemplateSelector x:Key="TemplateSelector"/>
    </Window.Resources>

This is equivalent to:

```csharp
    public DemoTemplateSelector DemoTemplateSelector = new DemoTemplateSelector();

When we refer to another namespace in code, we include that by including the using directive on top of our file:
```csharp
    using TemplateSelectorDemo.Selector;

In xaml the equivalent would be:

```xaml    
    xmlns:sel="clr-namespace:TemplateSelectorDemo.Selector"

Now the content of the _Label_ changes with the data type of the property in our ViewModel "CurrentViewModel", bound to the Content of the Label.

##Conclusion
The crucial steps to select the view you wish to display your data are:
* Create a class derrived from DataTemplateSelector
* Override the SelectTemplate method
* Declare the xml namespaces in your main view
* Choose a FrameworkElement having the Content propperty
* Give the content a ContentTemplateSelector based on your implementation of the DataTemplateSelector
* In that ContentTemplateSelector you can create your DataTemplates inline or using UserControls
</li>
