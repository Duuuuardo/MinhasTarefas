namespace MinhasTarefas;

public partial class MainPage : ContentPage
{
    private readonly List<CheckBox> _checkboxes = new();

    public MainPage()
    {
        InitializeComponent();
    }

    private void BtnAdicionar_Clicked(object sender, EventArgs e)
    {
        string descricao = EntryTarefa.Text;

        if (string.IsNullOrWhiteSpace(descricao))
        {
            DisplayAlert("Aviso", "Digite a descrição da tarefa.", "OK");
            return;
        }

        // Cria o CheckBox e a Label da nova tarefa
        var checkBox = new CheckBox
        {
            VerticalOptions = LayoutOptions.Center
        };

        var label = new Label
        {
            Text = descricao,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Start
        };

        // Associa o evento CheckedChanged no momento da criação
        checkBox.CheckedChanged += (s, e) =>
        {
            if (checkBox.IsChecked)
            {
                label.TextDecorations = TextDecorations.Strikethrough;
                label.TextColor = Colors.Gray;
            }
            else
            {
                label.TextDecorations = TextDecorations.None;
                label.TextColor = Colors.Default;
            }

            AtualizarContador();
        };

        // Agrupa CheckBox + Label em um HorizontalStackLayout
        var linha = new HorizontalStackLayout
        {
            Spacing = 10,
            Children =
            {
                checkBox,
                label
            }
        };

        // Adiciona a linha dentro da VerticalStackLayout da listagem
        ListaTarefas.Add(linha);
        _checkboxes.Add(checkBox);

        // Limpa o campo de texto
        EntryTarefa.Text = string.Empty;

        AtualizarContador();
    }

    private void AtualizarContador()
    {
        int concluidas = _checkboxes.Count(c => c.IsChecked);
        LblContador.Text = $"{concluidas} de {_checkboxes.Count} tarefas concluídas";
    }
}