# DataTemplateSelectorDemo #

Demonstrates how to use __DataTemplateSelector__ to create a data-aware user interface in XAML

***

In WPF we can use _type-dependant_ views by mapping a ___data type___ to a __data presentation__.

The UI is then able to insert the desired DataTemplate e.g. a _UserControl_ into the VisualTree 
of the current view based on the data-type it is bound to.

To help beginners understand how to employ this powerful _DataTemplateSelector_ this project
demonstrates some basics. They get you started. Play with your own ideas. You will figure it out.
  
So, we have a project with different types of data, each requiring a specific way of presention.

For example, if we have different types of 'person records' each specialized on say clients or suppliers,
we may want to use a specific 'mask'. One may focus on CRM, the other on purchasing.

In WPF we can do this.

The pivot to implementing such behavior is the _DataTemplateSelector_.

There are three simple steps to realize the _'data-awareness'_ in our application.

# Define the data

First, let's create the types we seek to use in our project. Since we use a _Binding_ to bring data into our view,
it is considerate to go with the MVVM pattern. The holy trinity Model, View, ViewModel. In this simple project, we amalgamate model and viewmodel. Econimize on classes.
As long as this classes implement the INotifyPropertyChanged interface, they will work.

We recommend to avoid the hazzle of creating your own MVVM framework. We did that. Now we use __CommunityToolkit.Mvvm__. There are others. Find what suits you best.

Now somewhere in our code we will retrieve data and present it to the user.

We have one view with controls and menues, bells and whistles - e.g. our main window. That view should be able to display different type of data, shouldn't it?

Let's use a property to bind the data to the view. Then let's assign the data retrieved to that property. They are of different types?

Can an ant become an elephant? Can a car become a bicycle? Chances are they can. Provided the types (classes) are convertable into each other.

If they descend from e.g. __object__, we're good. If _Ant_ and _Elephant_ are descendents of class _Animal_, that will do. Car and Bicycle are both _Vehicles_? Fine.

Let's illustrate that in c#: We have a base class 'Person' of which we derrive 'Client' and 'Supplier'. class Client : Person. class Supplier : Person.

The 'proxy' to the view is _CurrentViewModel_. It is a person. A person can become a Client or a Supplier. A Vehicle can be Car or a Bicycle. An Ant may identify as an Elepfhnat. Which is somehow related to an elephant. Or a typo. Blame me.

In our main view model we do something like this:

```csharp
    // this is in your main view model
    private Person currentViewModel;
    public Person CurrentViewModel { get => currentViewModel; set => SetProperty(currentViewModel, value, ()=> currentViewModel = x); }

    private Client client;
    public Client Client {get => client; set => SetProperty(client, value, ()=> client = x);

    private Supplier supplier;
    public Supplier Supplier {get => supplier; set => SetProperty(supplier, value, ()=> supplier = x);

    private void GetSupplier
    {
        Supplier = PersonFactory.GetSupplier();
        CurrentViewModel = Supplier;
    }

    private void GetClient
    {
        Client = PersonFactory.GetClient();
        CurrentViewModel = Client;
    }
```

Hint: Mind the casing of your fields and properties. A 'get => __C__ urrentViewModel' sends your app in an endless loop.
'get => __c__ urrentViewModel' it is. C and c. Different. 

Especially in the early morning hours after a lengthy coding session, this error might elude you. Go to bed. Sleep. Wake up, find the problem in a minute. That's software engineering.

# Create the doocot - pidgeonhole the data types using the TemplateSelector

After having defined our data types in our view models, we create now a new class derrived from DataTemplateSelector.
It's task is to decide which DataTemplate to use based on the type of data in the ViewModel.

There is a method we need to override.

```csharp
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
```

Our view will inquier what DataTemplate to use by sending the FrameworkElement 'container' in question along with the data 'item'.

SelectTemplate pideonholes the item returning the hole it went in. 

```csharp
﻿using System.Windows;
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
```

# Implement data templates in your main view #

We need a FrameworkElement which can bind to a DependencyProperty and is able to host a content.
One such FrameworkElement is the __Label__. There are others. Check your WPF tool case. 
The __Label__ element has an AttachedProperty __Content__ which we can either 'hard-code' in XAML or
make use of the __ContentTemplateSelector__.

We have created such TemplateSelector above.
Its interface to the VisualTree is the method __SelectTemplate__; based on the data type supplied it returns a _DataTemplate_.

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

When we refer to another namespace in code, we achieve that by including the  __using__  directive on top of our file:

```csharp
    using TemplateSelectorDemo.Selector;
```

In xaml the equivalent would be:

```xaml    
    xmlns:sel="clr-namespace:TemplateSelectorDemo.Selector"
```

Now the content of the _Label_ changes with the data type of the property in our ViewModel "CurrentViewModel", bound to the Content of the Label.

## Conclusion ##
The crucial steps to select the view you wish to display your data are:
* Define your data
** Make them convertible
* Create a class derrived from DataTemplateSelector
** Override the SelectTemplate method
* Declare the xml namespaces in your main view
** Choose a FrameworkElement having the Content propperty
** Give the content a ContentTemplateSelector based on your implementation of the DataTemplateSelector
** In that ContentTemplateSelector you can create your DataTemplates inline or using UserControls
