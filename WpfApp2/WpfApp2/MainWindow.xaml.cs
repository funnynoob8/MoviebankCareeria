using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Movie> movies;

        public MainWindow()
        {
            InitializeComponent();

            movies = new ObservableCollection<Movie>();
            LoadMovies();
            DataContext = new ViewModel(movies);
        }

        private void LoadMovies()
        {
            using (var db = new MovieDbContext())
            {
                var dbMovies = db.Movies.ToList();
                foreach (var movie in dbMovies)
                {
                    movies.Add(movie);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MovieDbContext())
            {
                if (DataContext is ViewModel vm)
                {
                    if (vm.SelectedMovie.Id == 0)
                    {
                        db.Movies.Add(vm.SelectedMovie);
                        movies.Add(vm.SelectedMovie);
                    }
                    else
                    {
                        db.Movies.Update(vm.SelectedMovie);
                    }
                    db.SaveChanges();
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel vm)
            {
                vm.SelectedMovie = new Movie();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel vm && vm.SelectedMovie.Id != 0)
            {
                using (var db = new MovieDbContext())
                {
                    db.Movies.Remove(vm.SelectedMovie);
                    db.SaveChanges();
                }
                movies.Remove(vm.SelectedMovie);
                vm.SelectedMovie = null;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel vm)
            {
                var query = from m in movies
                            where m.Title.Contains(searchBox.Text)
                            select m;
                vm.Movies = new ObservableCollection<Movie>(query);
            }
        }
    }
}



