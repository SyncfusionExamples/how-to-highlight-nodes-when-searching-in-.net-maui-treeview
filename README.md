# how-to-highlight-nodes-when-searching-in-.net-maui-treeview
This repository demonstrates how to highlight nodes when searching in the .NET MAUI TreeView (SfTreeView) control. It provides a sample implementation that dynamically updates the background color of TreeView nodes matching the search criteria, enabling a visually responsive and user-friendly search experience.

## Sample
You can highlight a TreeViewNode that match the search criteria in .NET MAUI TreeView by configuring the background property within the ItemTemplate.

Bind the model property to BackgroundColor property of Grid inside the ItemTemplate.

### XAML
```xaml
<Grid RowDefinitions="Auto,*">
    <SearchBar x:Name="searchBar" 
               Placeholder="Search TreeView"/>
    <syncfusion:SfTreeView x:Name="treeView"
                           Grid.Row="1" 
                           ChildPropertyName="SubFiles" 
                           ItemTemplateContextType="Node"
                           ItemHeight="25"
                           AutoExpandMode="AllNodesExpanded" 
                           ItemsSource="{Binding ImageNodeInfo}">
        <syncfusion:SfTreeView.ItemTemplate>
            <DataTemplate>
                <Grid x:Name="grid" 
                      RowSpacing="0" 
                      BackgroundColor="{Binding Content.NodeColor}">
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="20" />
                        <ColumnDefinition Width="*" />
                    </Grid.ColumnDefinitions>
                    <Image Source="{Binding Content.ImageIcon}" 
                           VerticalOptions="Center" 
                           HorizontalOptions="Center" 
                           HeightRequest="20" 
                           WidthRequest="20"/>
                    <Grid Grid.Column="1" 
                          RowSpacing="1" 
                          Padding="1,0,0,0" 
                          VerticalOptions="Center">
                        <Label LineBreakMode="NoWrap" 
                               FontSize="12" 
                               Text="{Binding Content.ItemName}" 
                               VerticalTextAlignment="Center"/>
                    </Grid>
                </Grid>
            </DataTemplate>
        </syncfusion:SfTreeView.ItemTemplate>
    </syncfusion:SfTreeView>
</Grid>
```

### Behavior

Set the BackgroundColor for the filtered items.
```csharp
public class ContentPageBehavior : Behavior<ContentPage>
{
    SearchBar SearchBar;
    SfTreeView TreeView;
    
    protected override void OnAttachedTo(ContentPage bindable)
    {
        SearchBar = bindable.FindByName<SearchBar>("searchBar");
        TreeView = bindable.FindByName<SfTreeView>("treeView");
        SearchBar.TextChanged += SearchBar_TextChanged;
        base.OnAttachedTo(bindable);
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        this.TraverseNodes(TreeView.Nodes, e.NewTextValue);
    }

    private void TraverseNodes(TreeViewNodeCollection nodes, string searchText)
    {
        foreach (var child in nodes)
        {
            (child.Content as FileManager).NodeColor = (child.Content as FileManager).ItemName.ToLower().StartsWith(searchText.ToLower()) ? Colors.Teal : Colors.Transparent;

            if (string.IsNullOrEmpty(searchText)) (child.Content as FileManager).NodeColor = Colors.Transparent;

            if (child.ChildNodes != null)
            {
                this.TraverseNodes(child.ChildNodes, searchText);
            }
        }
    }
}
```

## Requirements to run the demo

To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements).

## Troubleshooting:
### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

## License

Syncfusion® has no liability for any damage or consequence that may arise from using or viewing the samples. The samples are for demonstrative purposes. If you choose to use or access the samples, you agree to not hold Syncfusion® liable, in any form, for any damage related to use, for accessing, or viewing the samples. By accessing, viewing, or seeing the samples, you acknowledge and agree Syncfusion®'s samples will not allow you seek injunctive relief in any form for any claim related to the sample. If you do not agree to this, do not view, access, utilize, or otherwise do anything with Syncfusion®'s samples.
