using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace WpfApp2
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Movie> movies;
        private Movie selectedMovie;

        public ViewModel(ObservableCollection<Movie> movies)
        {
            this.movies = movies;
        }

        public ObservableCollection<Movie> Movies
        {
            get { return movies; }
            set
            {
                movies = value;
                OnPropertyChanged("Movies");
            }
        }

        public Movie SelectedMovie
        {
            get { return selectedMovie; }
            set
            {
                selectedMovie = value;
                OnPropertyChanged("SelectedMovie");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
