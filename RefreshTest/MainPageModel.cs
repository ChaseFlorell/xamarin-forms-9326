using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;

namespace RefreshTest
{
    public class MainPageModel : ReactiveObject
    {
        private ObservableAsPropertyHelper<bool> _isRefreshing;

        public MainPageModel()
        {
            Items = new List<string>
            {
                "one", "two","three"
            };

            RefreshCommand = ReactiveCommand.Create(SimulateWork);

            this.WhenAnyObservable(x => x.RefreshCommand.IsExecuting)
                .ToProperty(this, x => x.IsRefreshing, out _isRefreshing);
        }

        public IList<string> Items { get; set; }
        public ReactiveCommand<Unit,Unit> RefreshCommand { get; }
        public bool IsRefreshing => _isRefreshing.Value;

        private async void SimulateWork() => await Task.Delay(2000);
    }
}
