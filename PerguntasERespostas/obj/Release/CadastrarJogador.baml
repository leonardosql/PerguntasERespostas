   M S B A M L   `   `   `  ���� N  JPerguntasERespostas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PerguntasERespostasPerguntasERespostas  *    $PerguntasERespostas.CadastrarJogador   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:PerguntasERespostas       � Title$  CadastrarJogador��$	��660��$	��800��$	��660��$	��660��$	��800��$	��800�� � WindowStartupLocation$ CenterScreen=�.��� -   ����  �� ImageSource$ 01.gif�� �� Opacity$	 0.6Q� � ColumnDefinitions �� $	��81* ��� $��7* � � RowDefinitions �� .+��� -    ��Name$ 	lblTitulo��$��Cadastrar Jogadores��$
��10,0q�$	��Top=�$��48(�$��Center=�$
��Bold"�$��Verdana)�$��66��$��2���� -   $ lblCod��$��Código��$��	18,89,0,0q�$	��Top=�$��Right=�$��26��$
��Left=�$	��103���� -    ��Name$ txtCod��$��23��$��126,89,503.333,0q� �� TextWrapping$
 Wrap=�$	��Top=�{�.  $��1��.������ 	 �� 
TargetType"	 ��
 �� Triggers
 P�  P� Property � {� P� Value .  . ���  �� Property � �� �� Value �#eff3f3.���� -   $ lblNom��$
��Nome��$��
13,123,0,0q�$	��Top=�$��Right=�$
��Bold"�$��26��$
��Left=�$	��108���� -   $ txtNom��$��23��$��126,123,139.333,0q�$��1��$
 Wrap=�$�� ��$	��Top=�&   System.Windows.Controls.DataGrid  -     Name$ dgGrid��$��
14,298,9,0q���.     SelectionUnit$ FullRow=�$	��Top=�   
IsReadOnly . $	��266�� �� AlternationCount$ 2��   RowBackground
 �����   AlternatingRowBackground
 �����   CanUserReorderColumns .     CanUserDeleteRows .     CanUserAddRows .     CanUserResizeRows .   J� IsHitTestVisible .    SelectionMode$ Single=�   AreRowDetailsFrozen . 
�������   GridLinesVisibility$
 None=�$��2������ �� -    ��Name$ btnApagarTodos��$��Apagar todos Jogadores��$
��Left=�$��
13,579,0,0q�$	��Top=�$	��167��	� -	    	�Name$ 
radEquipeA��$��A�� 	� 	GroupName$ Equipe��$
��Left=�$��129,162,0,0q�$	��Top=�$��2��	� -
   $ 
radEquipeB��$��B��$ Equipe��$
��Left=�$��173,162,0,0q�$	��Top=�$��3��  J� RenderTransformOrigin$  0.051,-0.319&��� -   $ lblNom_Copy��$��Equipe��$��
13,156,0,0q�$	��Top=�$��Right=�$
��Bold"�$��26��$
��Left=�$	��108���� -   $ 	txtImagem��$��23��$��126,193,173.333,0q�$
 Wrap=�$�� ��$	��Top=�$��4���� -   $ lblNom_Copy1��$
��Foto��$��
14,190,0,0q�$	��Top=�$��Right=�$
��Bold"�$��26��$
��Left=�$	��108���� -   $ 
btnExcluir��$��Excluir��$��7��$
��Left=�$��294,270,0,0q�$	��Top=�$��75��$  0.727,0.354&��� -   $ btnAdicionar��$��Adicionar/Salvar��$��8��$
��Left=�$��384,270,0,0q�$	��Top=�$	��110��$  0.727,0.354&��� -   ! ��Name$! borderA1��$��1q����	#FF000000$��606,89,23,376q�$��2��._��� -   $��
imgAjudaA1��$
��Fill=����� " �� Path$" ActualHeight�# �� ElementName$# borderA1������ $" ActualWidth�$# borderA1��$ J� OpacityMask$ �� % �� EndPoint$% 0.5,1&�& �� 
StartPoint$& 0.5,0&�.��� ' � Color$' Black��( � Offset$( 0Q�� $' White��$( 1Q��� -   $ btnPesquisar��$	��...��$
��Left=�$��561,193,0,0q�$	��Top=�$��5��$��29��$  0.727,0.354&��� -   $ labelss��$��	Equipe A:��$
��Left=�$��695,573,0,0q�$	��Top=�$��2���� -   $ lblToTA��$��0��$
��Left=�$��30.667,573,0,0q�$	��Top=�$��1���� -   $ labelsss��$��	Equipe B:��$
��Left=�$��696,593,0,0q�$	��Top=�$��2���� -   $ lblToTB��$��0��$
��Left=�$��30.667,593,0,0q�$	��Top=�$��1���� -   $ btnNovo��$
��Novo��$��6��$
��Left=�$��203,270,0,0q�$	��Top=�$��75��$  0.727,0.354&��� -   ) ��Name$) cbxCategoria��$
��Left=�$��126,230,0,0q�$	��Top=�$	��246��$��23��$��5��* � DataContext* �� ���� �� -   $ lblCat��$��	Categoria��$��
14,230,0,0q�$	��Top=�$��Right=�$
��Bold"�$��26��$
��Left=�$	��108��