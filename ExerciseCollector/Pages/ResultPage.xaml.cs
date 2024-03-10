using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExerciseCollector.Entities;

namespace ExerciseCollector.Pages;

public partial class ResultPage : ContentPage
{
    public ICollection<ExerciseResult> ExerciseResults { get; }
    
    public int SummaryScore { get; }

    public ResultPage(ICollection<ExerciseResult> exerciseResults)
    {
        ExerciseResults = exerciseResults;
        SummaryScore = ExerciseResults.Select(r => r.Result).Sum();
        InitializeComponent();
    }
}
