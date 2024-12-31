using Syncfusion.Maui.TreeView;
using Syncfusion.TreeView.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUITreeView
{
    public class ContentPageBehavior : Behavior<ContentPage>
    {
        #region Fields

        SearchBar SearchBar;
        SfTreeView TreeView;

        #endregion

        #region Overrrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            SearchBar = bindable.FindByName<SearchBar>("searchBar");
            TreeView = bindable.FindByName<SfTreeView>("treeView");

            SearchBar.TextChanged += SearchBar_TextChanged;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            SearchBar.TextChanged -= SearchBar_TextChanged;
            this.SearchBar = null;
            this.TreeView = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region Events

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.TraverseNodes(TreeView.Nodes, e.NewTextValue);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Recursively traverse all the nodes in the TreeView and sets the BackgroundColor for the searched nodes. 
        /// </summary>
        /// <param name="nodes">Represents the TreeViewNodes.</param>
        /// <param name="searchText">Represents the search key word.</param>
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
        #endregion

    }
}
