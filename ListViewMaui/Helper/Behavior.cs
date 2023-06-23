using Syncfusion.Maui.DataSource;
using Syncfusion.Maui.ListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMaui
{
    public class Behavior : Behavior<ContentPage>
    {
        SfListView ListView;
        Label headerLabel;
        StackLayout Stack;
        Label deleteLabel;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");
            headerLabel = bindable.FindByName<Label>("headerLabel");
            Stack = bindable.FindByName<StackLayout>("stackLayout");
            deleteLabel = bindable.FindByName<Label>("deleteLabel");
            ListView.ItemDragging += ListView_ItemDragging;
            base.OnAttachedTo(bindable);
        }

        private async void ListView_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            var viewModel = this.ListView.BindingContext as ViewModel;

            if (e.Action == DragAction.Start)
            {
                this.headerLabel.IsVisible = false;
            }


            if (e.Action == DragAction.Dragging)
            {
                var position = new Point(e.Position.X - this.ListView.Bounds.X, Math.Abs(e.Position.Y - this.ListView.Bounds.Y));
                if ((this.Stack.Bounds.Y < position.Y) && (this.Stack.Bounds.Y + this.Stack.Height) > position.Y)
                {
                    this.deleteLabel.TextColor = Colors.Red;
                }
                else
                    this.deleteLabel.TextColor = Colors.White;
            }

            if (e.Action == DragAction.Drop)
            {
                var position = new Point(e.Position.X - this.ListView.Bounds.X, Math.Abs(e.Position.Y - this.ListView.Bounds.Y));

                if ((this.Stack.Bounds.Y < position.Y) && (this.Stack.Bounds.Y + this.Stack.Height) > position.Y)
                {
                    await Task.Delay(100);
                    viewModel.ToDoList.Remove(e.DataItem as ToDoItem);
                }
                this.deleteLabel.TextColor = Colors.White;
                this.headerLabel.IsVisible = true;
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            ListView.ItemDragging -= ListView_ItemDragging;
            ListView = null;
            headerLabel = null;
            Stack = null;
            deleteLabel = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
