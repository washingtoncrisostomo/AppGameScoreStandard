using AppGameScoreStandard.Model;
using AppGameScoreStandard.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppGameScoreStandard
{
    public partial class MainPage : ContentPage
    {
        private GameScore score;
        private GameScoreApi api;

        public MainPage()
        {
            InitializeComponent();
            api = new GameScoreApi();
        }

        private async void btLocalizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                score = await api.GetHighScore(Convert.ToInt32(entId.Text));
                if (score.id > 0)
                {
                    entHiScore.Text = score.highscore.ToString();
                    entGame.Text = score.game;
                    entName.Text = score.name;
                    entPhrase.Text = score.phrase;
                    entEmail.Text = score.email;
                    btSalvar.Text = "Atualizar";
                }
                else btSalvar.Text = "Cadastrar";
            }
            catch(Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }
        }

        private async void btExcluir_Clicked(object sender, EventArgs e)
        {
            try
            {
                score = await api.GetHighScore(Convert.ToInt32(entId.Text));
                if (score.id > 0)
                {
                    await api.DeleteHighScore(score.id);
                }
                await DisplayAlert("Alerta", "Operação realizada com sucesso","OK");
                this.LimparCampos();
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }
        }

        private async void btSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                score = new GameScore();
                score.highscore = Convert.ToInt32(entHiScore.Text);
                score.game = entGame.Text;
                score.name = entName.Text;
                score.email = entEmail.Text;
                score.phrase = entPhrase.Text;
                if(btSalvar.Text == "Atualizar")
                {
                    score.id = Convert.ToInt32(entId.Text);
                    api.UpDateHighScore(score);
                }
                else
                {
                    await api.CreateHighScore(score);
                }
                await DisplayAlert("Alerta", "Operação realizada com sucesso", "OK");
                this.LimparCampos();
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }
        }

        private void LimparCampos()
        {
            entId.Text = "";
            entHiScore.Text = "";
            entGame.Text = "";
            entName.Text = "";
            entPhrase.Text = "";
            entEmail.Text = "";
        }
    }
}
