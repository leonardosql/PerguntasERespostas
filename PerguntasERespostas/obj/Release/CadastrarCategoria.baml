   M S B A M L   `   `   `  ���� N  JPerguntasERespostas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PerguntasERespostasPerguntasERespostas  ,    &PerguntasERespostas.CadastrarCategoria   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:PerguntasERespostas       � Title$  Cadastrar Categoria��$��464.887��$	��800��$��464.887��$��464.887��$	��800��$	��800�� � WindowStartupLocation$ CenterScreen=�.��� -   ����  �� ImageSource$ 
Slide1.JPG�� � RowDefinitions �� .+��� -    ��Name$ 	lblTitulo��$.��(Cadastrar o Tema Relacionado a Dinâmica��$
��10,0q�$	��Top=�$��24(�$��Center=�$
��Bold"�$��Verdana)�$��79���� -   $ lblCod��$��Código��$��
67,130,0,0q�$	��Top=�$��Right=�$��26��$
��Left=�$��55�� J� RenderTransformOrigin$ 0.5,0.5&�$��6(�z�V� .~��� �� ��  �� Angle$ -1.289Q�T� �� -    ��Name$ txtCod��$��23��$��122,123,605,0q� �� TextWrapping$
 Wrap=�$	��Top=�{�.  $��1��.������ 	 �� 
TargetType"	 ��
 �� Triggers
 P�  P� Property � {� P� Value .  . ���  �� Property � �� �� Value �#eff3f3.���� -   $ lblNom��$
��Nome��$��103,118,0,0q�$	��Top=�$��Right=�$��42��$
��Left=�$��79��$
��Bold"�$��18(��� -   $ txtNom��$��23��$��187,123,202,0q�$��1��$
 Wrap=�$�� ��$	��Top=�&   System.Windows.Controls.DataGrid  -     Name$ dgGrid��$��41,224,22,0q���.     SelectionUnit$ FullRow=�$	��Top=�   
IsReadOnly . $	��170�� �� AlternationCount$ 2��   RowBackground
 �����   AlternatingRowBackground
 �����   CanUserReorderColumns .     CanUserDeleteRows .     CanUserAddRows .     CanUserResizeRows .   J� IsHitTestVisible .    SelectionMode$ Single=�   AreRowDetailsFrozen . 
�������   GridLinesVisibility$
 None=����� ����  �� Color$ 	#FFF0F0F0�� �� Opacity$	 0.6Q��� -    ��Name$ 
btnExcluir��$��Excluir��$��7��$
��Left=�$��321,156,0,0q�$	��Top=�$��75��$ 0.5,0.5&����	#FFF0EAEA$��16(�$
��Bold"�����   �� EndPoint$  0.5,1&�! �� 
StartPoint$! 0.5,0&�.��� " � Color$" 	#FF0BD326��# � Offset$# 0.294Q�� $" 	#FF0763EC��$# 0.739Q���
� .��� $" Black��$# 1Q�� $" 	#FF0BD326���� -	   $ btnAdicionar��$��Adicionar/Salvar��$��8��$
��Left=�$��445,156,0,0q�$	��Top=�$	��145��$ 0.727,0.354&����	#FFF0EAEA$��16(�$
��Bold"����� $  0.5,1&�$! 0.5,0&�.��� $" 	#FF0BD326��$# 0.294Q�� $" 	#FF0763EC��$# 0.739Q���
� .��� $" Black��$# 1Q�� $" 	#FF0BD326���� -
   $ btnNovo��$
��Novo��$��6��$
��Left=�$��187,156,0,0q�$	��Top=�$��75��$ 0.727,0.354&����	#FFF0EAEA$��16(�$
��Bold"����� $  0.5,1&�$! 0.5,0&�.��� $" 	#FF0BD326��$# 0.294Q�� $" 	#FF0763EC��$# 0.739Q���
� .��� $" Black��$# 1Q�� $" 	#FF0BD326��