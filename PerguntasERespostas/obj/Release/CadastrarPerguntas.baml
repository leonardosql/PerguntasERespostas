   M S B A M L   `   `   `  ���� N  JPerguntasERespostas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PerguntasERespostasPerguntasERespostas  ,    &PerguntasERespostas.CadastrarPerguntas   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:PerguntasERespostas       � Title$  CadastrarPergunta��$	��660��$	��800��$	��660��$	��660��$	��800��$	��800�� � WindowStartupLocation$ CenterScreen=�.��� -    � RowDefinitions �� .+��� -    ��Name$ 	lblTitulo��$��Cadastrar Perguntas��$��
10,34,10,0q�$	��Top=�$��18(�$��Center=�$
��Bold"�$��Verdana)�$��32���� -   $ lblCod��$��Código��$��	18,89,0,0q�$	��Top=�$��Right=�$��26��$
��Left=�$	��103���� -    ��Name$ txtCod��$��23��$��126,89,566,0q� �� TextWrapping$
 Wrap=�$	��Top=�{�.  $��1��.������  �� 
TargetType" �� �� Triggers P�  P� Property � {�	 P� Value	 .  . ��� 
 �� Property
 � �� �� Value �#eff3f3.���� -   $ lblPergunta��$��Questão��$��
13,123,0,0q�$	��Top=�$��Right=�$
��Bold"�$��26��$
��Left=�$	��108���� -   $ txtPer��$��23��$��126,123,27,0q�$��1��$
 Wrap=�$�� ��$	��Top=�&   System.Windows.Controls.DataGrid  -     Name$ dgGrid��$��
14,386,9,0q���.     SelectionUnit$ FullRow=�$	��Top=�   
IsReadOnly . $	��224�� �� AlternationCount$ 2��   RowBackground
 �����   AlternatingRowBackground
 �����   CanUserReorderColumns .     CanUserDeleteRows .     CanUserAddRows .     CanUserResizeRows .   J� IsHitTestVisible .    SelectionMode$ Single=�   AreRowDetailsFrozen . 
�������   GridLinesVisibility$
 None=����� �� -    ��Name$ 
btnExcluir��$��Excluir��$��7��$
��Left=�$��355,350,0,0q�$	��Top=�$��75�� J� RenderTransformOrigin$ 0.5,0.5&��� -	   $ btnAdicionar��$��Adicionar/Salvar��$��8��$
��Left=�$��445,350,0,0q�$	��Top=�$	��110��$ 0.727,0.354&��� -
   $ btnNovo��$
��Novo��$��6��$
��Left=�$��264,350,0,0q�$	��Top=�$��75��$ 0.727,0.354&��� -   $ lblResA��$��Resposta a)��$��
13,158,0,0q�$	��Top=�$��Right=�$
��Bold"�$��26��$
��Left=�$	��108���� -   $ txtRespA��$��23��$��126,158,27,0q�$��1��$
 Wrap=�$�� ��$	��Top=��� -   $ lblResB��$��Resposta b)��$��
13,192,0,0q�$	��Top=�$��Right=�$
��Bold"�$��26��$
��Left=�$	��108���� -   $ txtRespB��$��23��$��126,192,27,0q�$��1��$
 Wrap=�$�� ��$	��Top=��� -   $ lblResC��$��Resposta c)��$��
13,228,0,0q�$	��Top=�$��Right=�$
��Bold"�$��26��$
��Left=�$	��108���� -   $ txtRespC��$��23��$��126,228,27,0q�$��1��$
 Wrap=�$�� ��$	��Top=��� -   $ lblResD��$��Resposta d)��$��
13,263,0,0q�$	��Top=�$��Right=�$
��Bold"�$��26��$
��Left=�$	��108���� -   $ txtRespD��$��23��$��126,263,27,0q�$��1��$
 Wrap=�$�� ��$	��Top=�	� -    	�Name$
 radA��$��A�� 	� 	GroupName$ Equipe��$
��Left=�$��134,300,0,0q�$	��Top=�$��2��	� -   $
 radB��$��B��$ Equipe��$
��Left=�$��178,300,0,0q�$	��Top=�$��3��$ 0.051,-0.319&�	� -   $
 radC��$��C��$ Equipe��$
��Left=�$��224,300,0,0q�$	��Top=�$��3��$ 0.051,-0.319&�	� -   $
 radD��$��D��$ Equipe��$
��Left=�$��264,300,0,0q�$	��Top=�$��3��$ 0.051,-0.319&��� -   $ lblNom_Copy��$��Resposta Correta��$��
13,294,0,0q�$	��Top=�$��Right=�$
��Bold"�$��26��$
��Left=�$	��108���� -    ��Name$ cbxCategoria��$
��Left=�$��519,294,0,0q�$	��Top=�$	��246��$��23��$��5�� � DataContext �� ���� �� -   $ lblCat��$��	Categoria��$��406,294,0,0q�$	��Top=�$��Right=�$
��Bold"�$��26��$
��Left=�$	��108��