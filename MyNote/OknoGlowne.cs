using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace MyNote
{
    public partial class OknoGlowne : Form
    {
        //Zmienne
        public const string NazwaProgramu = "MyNote wersja 1.0";
        bool DokonanoModyfikacji = false;
        DateTime czasPodczasZalaczenia;
        public OknoGlowne()
        {
            this.Text = NazwaProgramu;
            InitializeComponent();
            czasPodczasZalaczenia = DateTime.Now;
            Przycisk_MG_WybierzStrone.SelectedIndex = 0;
            KontenerGlowny.Panel2Collapsed = true;
        }

        private void Przycisk_MG_Nowydokument_Click(object sender, EventArgs e)
        {
            //Sprawdzenie, czy nie ma już pustej strony
            if (TekstGlowny.Text.Length == 0) return;
            //Wyświetlenie komunikatu przed stworzeniem nowego dokumentu
            if (MessageBox.Show("Czy na pewno chcesz otorzyć nowy, czysty dokument?",
                "Nowy dokument", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            TekstGlowny.Clear();
            DokonanoModyfikacji = false;
        }

        private void Przycisk_MG_OtworzDokument_Click(object sender, EventArgs e)
        {
            //Otwieranie okna OtwórzPlik
            if (OtworzPlik.ShowDialog() != DialogResult.OK) return;
            // Wczytaj ściezkę do pliku
            TekstGlowny.LoadFile(OtworzPlik.FileName);
            this.Text = NazwaProgramu + " - " + OtworzPlik.SafeFileName;
            DokonanoModyfikacji = false;
        }

        private void Przycisk_MG_ZapiszDokument_Click(object sender, EventArgs e)
        {
            //Otwieranie okna zapisu
            if (ZapiszPlik.ShowDialog() != DialogResult.OK) return;
            //Zapisz plik
            TekstGlowny.SaveFile(ZapiszPlik.FileName);
            this.Text = NazwaProgramu + " - " + ZapiszPlik.FileName;
            DokonanoModyfikacji = false;
        }
        //Wyrównanie do Lewej, Środka, Prawej
        private void Przycisk_MG_DoLewej_Click(object sender, EventArgs e)
        {
            TekstGlowny.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void Przycisk_MG_DoSrodka_Click(object sender, EventArgs e)
        {
            TekstGlowny.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void Przycisk_MG_DoPrawej_Click(object sender, EventArgs e)
        {
            TekstGlowny.SelectionAlignment = HorizontalAlignment.Right;
        }
        //Pogrubienie
        private void Przycisk_MG_PogrobTekst_Click(object sender, EventArgs e)
        {
            if (CzcionkiTekstu.ShowDialog() == DialogResult.OK)
            {
                TekstGlowny.SelectionFont = CzcionkiTekstu.Font;
            }
        }

        private void Przycisk_MG_KolorTekst_Click(object sender, EventArgs e)
        {
            if (KolorTekstu.ShowDialog() == DialogResult.OK)
            {
                TekstGlowny.SelectionColor = KolorTekstu.Color;
            }
        }

        private void Przycisk_MG_WypelnienieKolor_Click(object sender, EventArgs e)
        {
            if (KolorTekstu.ShowDialog() == DialogResult.OK)
            {
                TekstGlowny.SelectionBackColor = KolorTekstu.Color;
            }
        }

        private void Przycisk_MG_DodajObrazek_Click(object sender, EventArgs e)
        {
            if (OtworzObraz.ShowDialog() != DialogResult.OK) return;
            //Tworzenie kopii schowka
            IDataObject schowekTymczasowy = Clipboard.GetDataObject();
            //Pobieranie obrazu
            Image img = Image.FromFile(OtworzObraz.FileName);
            //Dodawanie obrazu do schowka
            Clipboard.SetImage(img);
            TekstGlowny.Paste();
            Clipboard.SetDataObject(schowekTymczasowy);
        }
        //Zaznaczenie tekstu w dokumencie oraz wybranie w rozwijanym pasku
        //strony i przejście do niej
        private void Przycisk_MG_IdzDoStrony_Click(object sender, EventArgs e)
        {
            if (TekstGlowny.Text.Length == 0)
            {
                MessageBox.Show("Nie zaznaczyłeś(aś) tekstu");
                return;
            }
            KontenerGlowny.Panel2Collapsed = false;
            string text = TekstGlowny.SelectedText.Replace(" ", "%20");
            string Wikipedia = "https://pl.wikipedia.org/wiki/";
            string GoogleTranslator = "https://translate.google.pl/";
            int aktualnyWybor = Przycisk_MG_WybierzStrone.SelectedIndex;
            if (aktualnyWybor == 0) Web.Navigate(Uri.EscapeUriString(Wikipedia) + text);
            if (aktualnyWybor == 1) Web.Navigate(Uri.EscapeUriString(GoogleTranslator) + text);

        }
        //Zwijanie i rozwijanie panelu Internetu
        private void Przycisk_MG_ChowajWyszukiwarke_Click(object sender, EventArgs e)
        {
            KontenerGlowny.Panel2Collapsed = !KontenerGlowny.Panel2Collapsed;
        }

        private void Przycisk_MG_OMnie_Click(object sender, EventArgs e)
        {
            OProgramie oProgramie = new OProgramie(this);
            oProgramie.Show();
            this.Visible = false;
        }
        //Wyświetlanie czasu pracy
        private void Timer_Tick(object sender, EventArgs e)
        {
            int czasWMinutach = (int)(DateTime.Now - czasPodczasZalaczenia).Minutes;
            WyswietlCzasPracy.Text = "[Czas pracy : " + czasWMinutach + " minut]";
        }
        //Wyświetlanie ilosci akapitów i słów
        private void TekstGlowny_TextChanged(object sender, EventArgs e)
        {
            int akapity = TekstGlowny.Lines.Length;
            int slowa = TekstGlowny.Text.Length;
            WyswietlIloscTekstu.Text = "[Akapitów : " + akapity + " znaków : " + slowa + "}";
            DokonanoModyfikacji = true;
        }
        //WEB
        private void Web_Przycisk_Nawiguj_Click(object sender, EventArgs e)
        {
            Web.Navigate(Wyszukiwarka.Text);
        }

        private void Web_Przycisk_Wroc_Click(object sender, EventArgs e)
        {
            Web.GoBack();
        }

        private void Web_Przycisk_DoPrzodu_Click(object sender, EventArgs e)
        {
            Web.GoForward();
        }
        //Przyciski prawej myszy

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TekstGlowny.Copy();
        }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TekstGlowny.Cut();
        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TekstGlowny.Paste();
        }

        private void cofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TekstGlowny.Undo();
        }

        private void ponówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TekstGlowny.Redo();
        }

        private void OknoGlowne_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DokonanoModyfikacji) return;
            DialogResult r = MessageBox.Show("Czy chcesz zapisać przed zamknięciem?", "Zamykanie",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (r == DialogResult.Cancel) e.Cancel = true; // Przerwanie zamykania
            if (r == DialogResult.Yes) Przycisk_MG_ZapiszDokument_Click(sender, e);
        }

        private void zaznaczWszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TekstGlowny.SelectAll();
        }

        private void Form1_FormClosing(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
