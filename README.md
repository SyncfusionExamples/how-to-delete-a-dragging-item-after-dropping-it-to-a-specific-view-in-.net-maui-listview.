# how-to-delete-a-dragging-item-after-dropping-it-to-a-specific-view-in-.net-maui-listview.

This example demonstrates about how to delete a dragging item after dropping it into a specific view in .NET MAUI ListView (SfListView).

## XAML
    <syncfusion:SfListView x:Name="listView" Grid.Row="1" 
                            ScrollBarVisibility="Never"
                        ItemSize="60"
                        BackgroundColor="#FFE8E8EC"
                        GroupHeaderSize="50"
                        ItemsSource="{Binding ToDoList}"
                        DragStartMode="OnHold,OnDragIndicator"
                        SelectionMode="None">
        <syncfusion:SfListView.ItemTemplate>
            <DataTemplate>
                <Grid  BackgroundColor="White">
                    ---

                    ---
                    <syncfusion:DragIndicatorView Grid.Column="2" ListView="{x:Reference listView}" HorizontalOptions="Center" VerticalOptions="Center">
                        <Grid Padding="10, 20, 20, 20">
                            <Image Source="dragindicator.png" VerticalOptions="Center" HorizontalOptions="Center" />
                        </Grid>
                    </syncfusion:DragIndicatorView>
                </Grid>
            </DataTemplate>
        </syncfusion:SfListView.ItemTemplate>
        <syncfusion:SfListView.DragItemTemplate>
            <DataTemplate>
                <Grid RowDefinitions="60" BackgroundColor="White" Opacity="0.25">
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="70" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="60" />
                    </Grid.ColumnDefinitions>
                    <Grid Padding="25,20,25,20">
                        <Grid.GestureRecognizers>
                            <TapGestureRecognizer Command="{Binding Path=BindingContext.MarkDoneCommand, Source={x:Reference Name=listView}}" CommandParameter="{Binding .}"/>
                        </Grid.GestureRecognizers>
                        <Image Source="checked.png" HorizontalOptions="Center" VerticalOptions="Center" Aspect="AspectFill" IsVisible="{Binding IsDone}"/>
                        <Image Source="unchecked.png" HorizontalOptions="Center" VerticalOptions="Center" Aspect="AspectFill"
                    IsVisible="{Binding IsDone, Converter={StaticResource inverseBoolConverter}}"/>
                    </Grid>
                    <Label x:Name="textLabel" Text="{Binding Name}" Grid.Column="1" FontSize="15" TextColor="#333333" VerticalOptions="Center" HorizontalOptions="Start" Margin="5,0,0,0" />
                    <BoxView Grid.Column="1" Margin="5,3,0,0" BackgroundColor="#333333" HeightRequest="1" WidthRequest="{Binding Source={x:Reference textLabel}, Path=Width}"
                    VerticalOptions="Center" HorizontalOptions="Start" IsVisible="{Binding IsDone}" />
                    <syncfusion:DragIndicatorView Grid.Column="2" ListView="{x:Reference listView}" HorizontalOptions="Center" VerticalOptions="Center">
                        <Grid Padding="10, 20, 20, 20">
                            <Image Source="dragindicator.png" VerticalOptions="Center" HorizontalOptions="Center" />
                        </Grid>
                    </syncfusion:DragIndicatorView>
                </Grid>
            </DataTemplate>
        </syncfusion:SfListView.DragItemTemplate>
    </syncfusion:SfListView>
## C#

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

## Requirements to run the demo

* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) or [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/)
* Xamarin add-ons for Visual Studio (available via the Visual Studio installer).

## Troubleshooting

### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.