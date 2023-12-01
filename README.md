# DataTemplateSelectorDemo #

Demonstrates how to use __DataTemplateSelector__ to create a data-aware user interface in XAML

***

In Xaml we can use _type-driven_ views by mapping a __view__ to a ___data type___.
The UI is then able to insert the desired DataTemplate e.g. a _UserControl_ into the VisualTree 
of the current view based on the data-type it is bound to.

To help beginners understand how to employ this powerful _DataTemplateSelector_ this project
demonstrates some basics. They get you started. Play with your own ideas. You will figure it out.
  
So, we have a project with different types of data, each requiring a specific way of presention.

For example, if we have different types of 'person records' each specialized on say clients or suppliers,
we may want to use a specific 'mask'. One may focus on CRM, the other on purchasing.

In WPF we can do this.

The pivot to implementing such behavior is the _DataTemplateSelector_.

There are two parts in order to realize the _'data-awareness'_ in our application.

#Implement data templates in your main view#

You need a FrameworkElement which can bind to a DependencyProperty and is able to host a content.
One such FrameworkElement is the __Label__. There are others. Check your WPF tool case. 
The __Label__ element has an AttachedProperty __Content__ which we can either 'hard-code' in XAML or
make use of the __ContentTemplateSelector__.

First, let's create the types we seek to use in our project. Since we use a _Binding_ to bring data into our view,
it is considerate to go with the MVVM pattern. The holy trinity Model, View, ViewModel. In this simple project, we amalgamate model and viewmodel.
As long as this classes implement the INotifyPropertyChanged interface, they will work.

We recommend to avoid the hazzle of creating your own MVVM framework. We did that. Now we use __CommunityToolkit.Mvvm__. There are others. Find what suits you best.

After having defined our view models, we create a class derrived from DataTemplateSelector.
It's task is to decide which DataTemplate to use based on the type of data in the ViewModel.
Needless to say, the types (classes) must be convertable into each other. If they descend from e.g. __object__, we're good.
To be more practical, we could have a base class 'PersonBase' of which we derrive 'Client' and 'Supplier'.

```csharp
    // this is in your main view model
    private PersonBase currentViewModel;
    public PersonBase CurrentViewModel {get => currentViewModel; set => SetProperty(currentViewModel, value, ()=> currentViewModel = x);

    private Client client;
    public Client Client {get => client; set => SetProperty(client, value, ()=> client = x);

    private Supplier supplier;
    public Supplier Supplier {get => supplier; set => SetProperty(supplier, value, ()=> supplier = x);

    private void GetSupplier
    {
        CurrentViewModel

```

Hint: Mind the casing of your fields and properties. A 'get => __C__urrentViewModel' sends your app in an endless loop.
It's 'get => __c__urrentViewModel'.
Especially in the early morning hours after a lengthy coding session, this error might elude you. 

```csharp
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
```

It's interface to the VisualTree is the method __SelectTemplate__; based on the data type supplied it returns a _DataTemplate_.
This DataTemplate is __not__ a view or UserControl! It does not posses a VisualTree, we provide that DataTemplate either 'inline' or
we use a UserControl in our project.

The syntax is basically:

ElementWithContent.ContentTemplateSelector
    myTemplateSelector
        myTemplateSelector.Template1
            DataTemplate1
        myTemplateSelector.Template2
            DataTemplate2

where _DataTemplate1_ is what you implement when the selector tells you: 'Based on the data-type supplied I consider _Template1_ being the right fit'.

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
```

Now, where does the xmlns 'sel' come from?

In our example view _MainView_ we create an instance of our DemoDataSelector class by putting it in the window's resources.
```xaml
    <Window.Resources>
        <sel:DemoTemplateSelector x:Key="TemplateSelector"/>
    </Window.Resources>
```

This is equivalent to:

```csharp
    public DemoTemplateSelector DemoTemplateSelector = new DemoTemplateSelector();
```

When we refer to another namespace in code, we achieve that by including the __using__ directive on top of our file:

```csharp
    using TemplateSelectorDemo.Selector;
```

In xaml the equivalent would be:

```xaml    
    xmlns:sel="clr-namespace:TemplateSelectorDemo.Selector"
```

Now the content of the _Label_ changes with the data type of the property in our ViewModel "CurrentViewModel", bound to the Content of the Label.

##Conclusion
The crucial steps to select the view you wish to display your data are:
* Create a class derrived from DataTemplateSelector
* Override the SelectTemplate method
* Declare the xml namespaces in your main view
* Choose a FrameworkElement having the Content propperty
* Give the content a ContentTemplateSelector based on your implementation of the DataTemplateSelector
* In that ContentTemplateSelector you can create your DataTemplates inline or using UserControls
