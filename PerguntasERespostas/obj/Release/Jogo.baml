   M S B A M L   `   `   `  ���� N  JPerguntasERespostas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PerguntasERespostasPerguntasERespostas  $    PerguntasERespostas.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:PerguntasERespostas     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com    � WindowStyle$
  None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � Title$ 
MainWindow��$	��754��$
��1280������  �� ImageSource$ Image\quiz.jpg��.���� $y�Visible=�$	��754��$
��1280��.+��� $�� ��$
��1260�� �� Left$ 10��
 �� Top$	 578��$��31������ �� $�� ��$
��1260��$ 10��$	 616��$��31������ �� $�� ��$
��1260��$ 10��$	 658��$��31������ �� $�� ��$
��1260��$ 10��$	 537��$��31������ �� -    ��Name$ btnSair��$
��Sair��$
 1196��$ 2��$��74��	 � ToolTip$	 Sair do Jogo���� -   
 ��Name$
 lblVS��$��X��$��80(�$	 612��$	 105���� -   $
 lblTempo��$��10��$	��102(�$	 578��$	 235�����	#FFF12707$��	Condensed&�$
��Bold"�$	��140������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF2EF10F�� � Offset$
 0.13Q�� $ 	#FF19D2F0��$ 0.706Q��� -   $
 	lblTempo2��$��Tempo��$��20(�$	 612��$	 198��
��� E��$��76��$��Center=�$��Center=�$
��Bold"��� -   $ btnTempo��$��Start��$	 578��$	 380��$	��127��
������ ���	#FFF5F0EE
�������$��20(�$
��Bold"��� $��1q����	#FF000000$ 10��$	 474��$
��1260��._��� -   $
 lblPergunta��$��Pergunta��$��24(�$��50��$ 22��$	 470��$��0,0,0,0q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFF3EFEF��$ 0Q�� $ 	#FFF5F1F1��$ 1Q��� -   $
 
lblEquipeA��$��Equipe A��$��50(�$ 2��$ 26���� -   $
 
lblEquipeB��$��Equipe B��$��50(�$	 908��$ 26���� -	   $
 	lblPlacar��$!��1            x            0��$��Center=�$��50(�$	 374��$ 20��$	��526���� -
    ��Name$ borderA��$��1q����	#FF000000$	��Top=�$ 10��$ 98��$	��353��$	��351��._��� -    ��Name$ imgJogadorA��$
��Fill=�$	��Top=����� *ActualWidth+ �� ElementName$ borderA������ *ActualHeight+$ borderA���� -   $ borderA1��$��1q����	#FF000000$	��Top=�$	 374��$ 98��$	��164��$	��163��._��� -   $ 
imgAjudaA1��$
��Fill=�$	��Top=����� *ActualWidth+$ borderA1������ *ActualHeight+$ borderA1��y��� *
Visibility+$ borderA1���� -   $ borderA2��$��1q����	#FF000000$	��Top=�$	 374��$	 287��$	��164��$	��163��._��� -   $ 
imgAjudaA2��$
��Fill=�$	��Top=����� *ActualWidth+$ borderA2������ *ActualHeight+$ borderA2��y��� *
Visibility+$ borderA2���� -   $ borderB��$��1q����	#FF000000$	��Top=�$	 919��$ 98��$	��353��$	��351��._��� -   $ imgJogadorB��$
��Fill=�$	��Top=����� *ActualWidth+$ borderB������ *ActualHeight+$ borderB���� -   $ borderB1��$��1q����	#FF000000$	��Top=�$	 737��$ 98��$	��164��$	��163��._��� -   $ 
imgAjudaB1��$
��Fill=�$	��Top=����� *ActualWidth+$ borderB1������ *ActualHeight+$ borderB1��y��� *
Visibility+$ borderB1���� -   $ borderB2��$��1q����	#FF000000$	��Top=�$	 737��$	 287��$	��164��$	��163��._��� -   $ 
imgAjudaB2��$
��Fill=�$	��Top=� J� RenderTransformOrigin$ 	0.994,0.5&����� *ActualWidth+$ borderB2������ *ActualHeight+$ borderB2��y��� *
Visibility+$ borderB2���� $��5q�$ 10��$	 526��$
��1260��.+�(� $��40��$
��Left=�.��	� -    	�Name$ 	RespostaA��$��Center=� 	� 	GroupName$ question��$��24(�$��40��
�������$��A) asdjaskdksajkdas��(� $��40��$
��Left=�.��	� -   $ 	RespostaB��$��Center=�$ question��$��24(�$��40��$��B) asdjaskdksajkdas��(� $��40��$
��Left=�.��	� -   $ 	RespostaC��$��Center=�$ question��$��24(�$��40��$��C) asdjaskdksajkdas��(� $��40��$
��Left=�$��Center=�.��	� -   $ 	RespostaD��$��Center=�$ question��$��24(�$��40��$��D) asdjaskdksajkdas���� -   $ btnResponder��$��	Responder��$	 541��$��24(�$	 707��$	��197��$��36��$
��Bold"�$��Center=�$��Center=�$��-2,1,1,1q�
���   ����� $ 0.5,1&�$ 0.5,0&�.��� $ Black��$ 0Q�� $ White������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFD5F505��$
 0.46Q�� $ 	#FF08EA98��$ 1Q��� -   $
 lblx1��$��X��$	��100(�
���  ��$	 374��$	 105��$	��157��$	��163��$��0q�$��Center=�$��Center=��� -   $
 lblx2��$��X��$	��100(�
���  ��$	 374��$	 287��$	��157��$	��163��$��0q�$��Center=�$��Center=��� -   $
 lblx3��$��X��$	��100(�
���  ��$	 737��$ 98��$	��157��$	��163��$��0q�$��Center=�$��Center=��� -   $
 lblx4��$��X��$	��100(�
���  ��$	 737��$	 290��$	��157��$	��163��$��0q�$��Center=�$��Center=�