using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EsportPlayerManager.Services;

namespace EsportPlayerManager.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ICommand ShowPlayerCommand { get; }
    public ICommand ShowTournamentsCommand { get; }
    public ICommand ShowTrainingCommand { get; }
    [ObservableProperty] private object? _currentView;


    [ObservableProperty] private bool _playerButtonEnabled;
    [ObservableProperty] private bool _tournamentsButtonEnabled = true;
    [ObservableProperty] private bool trainingButtonEnabled = true;

    
    public MainWindowViewModel()
    {
        ShowPlayerCommand = new RelayCommand(ShowPlayers);
        ShowTournamentsCommand = new RelayCommand(ShowTournaments);
        CurrentView = new PlayersViewModel();
    }


    private void ShowPlayers()
    {
        CurrentView = new PlayersViewModel();
        PlayerButtonEnabled = false;
        TournamentsButtonEnabled = true;
        TrainingButtonEnabled = true;
    }    
    
    private void ShowTournaments()
    {
        CurrentView = new TournamentsViewModel();
        PlayerButtonEnabled = true;
        TournamentsButtonEnabled = false;
        TrainingButtonEnabled = true;
    }    

}