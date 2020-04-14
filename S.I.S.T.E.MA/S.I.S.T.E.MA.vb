﻿Imports System.Net.Http
Imports Newtonsoft.Json
Public Class home

#Region "Declaração Variaveis"
    Dim arraste As Boolean
    Dim mouseX As Integer
    Dim mouseY As Integer
    Dim brightMode As Boolean 'False = Escuro / True = Claro
    Dim homeAtivo As Boolean = True
    Dim admAtivo As Boolean = False
    Dim cartaoAtivo As Boolean = False
    Dim vendAtivo As Boolean = False
    Dim relatAtivo As Boolean = False
    Dim prodAtivo As Boolean = False
    Dim infoAtivo As Boolean = False
    Dim msgBoxResposta As Boolean
    Public apiOperationResposta As Boolean
#Region "Declaração de variáveis usadas nas placeholdes"
    Dim controlePlace1 As Boolean
    Dim controlePlace2 As Boolean
    Dim controlePlace3 As Boolean
    Dim controlePlace4 As Boolean
    Dim controlePlace5 As Boolean
#End Region

#End Region

#Region "Load/Fechar/Minimizar/Movimentação/ModoDeCor"
    Private Sub home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim horaatual As Int32 = DateTime.Now.Hour
        horaatual = 20
        If (horaatual >= 6 And horaatual <= 17) Then
            brightMode = True
            pbBrightMode.Image = My.Resources.imgMoonIcon
        Else
            brightMode = False
            pbBrightMode.Image = My.Resources.imgSunIcon
        End If
        'alteraDesign()
        Me.FormBorderStyle = FormBorderStyle.None
        Me.Height = 459
        Me.Width = 884
        Dim p As New Drawing2D.GraphicsPath()
        p.StartFigure()
        p.AddArc(New Rectangle(0, 0, 30, 30), 180, 90)
        p.AddLine(30, 0, Me.Width - 30, 0)
        p.AddArc(New Rectangle(Me.Width - 30, 0, 30, 30), -90, 90)
        p.AddLine(Me.Width, 30, Me.Width, Me.Height - 30)
        p.AddArc(New Rectangle(Me.Width - 30, Me.Height - 30, 30, 30), 0, 90)
        p.AddLine(Me.Width - 30, Me.Height, 30, Me.Height)
        p.AddArc(New Rectangle(0, Me.Height - 30, 30, 30), 90, 90)
        p.CloseFigure()
        Me.Region = New Region(p)
    End Sub
    Private Sub pnBarraSuperior_MouseDown(sender As Object, e As MouseEventArgs) Handles pnBarraSuperior.MouseDown
        arraste = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub pnBarraSuperior_MouseMove(sender As Object, e As MouseEventArgs) Handles pnBarraSuperior.MouseMove
        If arraste Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub
    Private Sub pnBarraSuperior_MouseUp(sender As Object, e As MouseEventArgs) Handles pnBarraSuperior.MouseUp
        arraste = False
    End Sub
    Private Sub pnFechar_Click(sender As Object, e As EventArgs) Handles pnFechar.Click
        pbFechar_Click(sender, e)
    End Sub
    Private Sub pbFechar_Click(sender As Object, e As EventArgs) Handles pbFechar.Click
        Application.Exit()
    End Sub
    Private Sub pnMinimizar_Click(sender As Object, e As EventArgs) Handles pnMinimizar.Click
        pbMinimizar_Click(sender, e)
    End Sub

    Private Sub pbMinimizar_Click(sender As Object, e As EventArgs) Handles pbMinimizar.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Public Sub backHome()
        Me.Enabled = True
        Me.WindowState = FormWindowState.Normal
    End Sub



#End Region

#Region "Navegação Menu"

#Region "Modulo de Execucao"

    'Variaveis de Navegação
    'Funções:
    'SizeOpen = Estabelece tamanhado de submenu que será aberto
    'SizaClose = Estabelece tamanhado de submenu que será fechado
    'pnAnterior = Recebe de selectAtual() qual menu está aberto no momento
    'pnProximo(passado por parametro) = Recebe o panel que será aberto
    'pnAnteriorAux/pnProximoAux =  Recebe o panel aux do seu respectivo (apenas para .Refresh)
    'pnSubAnterior = Recebe o SubMenu que está aberto
    'pnSubProximo = Recebe o SubMenu que será aberto

    Public SizeOpen, SizeClose As Integer
    Public pnAnterior, pnAnteriorAux, pnProximoAux, pnSubAnterior, pnSubProximo As Panel
    Sub navegacaoMenu(nivel As Integer, pnProximo As Panel, pnToOpenScreen As Panel)

        FecharSubMenuScreenEBar(pnProximo)

        If nivel = 1 Then

            'Todo ".Refresh" atualiza o panel para evitar bug visual

            'De acordo com pnAnterior, pnAnteriorAux recebe seu respectivo panel...
            ' e SizeClose seu devido tamanho
            pnAnterior = selectAtual()
            If pnAnterior.Name = "pnHome" Then
                pnAnteriorAux = pnHomeAux
            ElseIf pnAnterior.Name = "pnAdm" Then
                pnAnteriorAux = pnAdmAux
                pnSubAnterior = pnSubMenuAdm
                SizeClose = 67
            ElseIf pnAnterior.Name = "pnCartao" Then
                pnAnteriorAux = pnCartaoAux
                pnSubAnterior = pnSubMenuCartao
                SizeClose = 132
            ElseIf pnAnterior.Name = "pnVend" Then
                pnAnteriorAux = pnVendAux
                pnSubAnterior = pnSubMenuVend
                SizeClose = 132
            ElseIf pnAnterior.Name = "pnProd" Then
                pnAnteriorAux = pnProdAux


            ElseIf pnAnterior.Name = "pnRelat" Then
                pnAnteriorAux = pnRelatAux


            ElseIf pnAnterior.Name = "pnInfo" Then
                pnAnteriorAux = pnInfoAux


            End If

            'De acordo com pnProximo, pnProxumoAux recebe seu respectivo panel...
            ' e SizeOpen seu devido tamanho
            If pnProximo.Name = "pnHome" Then
                pnProximoAux = pnHomeAux
            ElseIf pnProximo.Name = "pnAdm" Then
                pnProximoAux = pnAdmAux
                pnSubProximo = pnSubMenuAdm
                SizeOpen = 67
            ElseIf pnProximo.Name = "pnCartao" Then
                pnProximoAux = pnCartaoAux
                pnSubProximo = pnSubMenuCartao
                SizeOpen = 132
            ElseIf pnProximo.Name = "pnVend" Then
                pnProximoAux = pnVendAux
                pnSubProximo = pnSubMenuVend
                SizeOpen = 132
            ElseIf pnProximo.Name = "pnProd" Then
                pnProximoAux = pnProdAux


            ElseIf pnProximo.Name = "pnRelat" Then
                pnProximoAux = pnRelatAux


            ElseIf pnProximo.Name = "pnInfo" Then
                pnProximoAux = pnInfoAux


            End If

            'Fechar SubMenu aberto. PS: Home não tem Submenu
            'Caso Home, não executa
            If pnAnterior.Name <> "pnHome" Then
                While pnSubAnterior.Height > 0
                    If SizeClose = 67 Then
                        pnSubAnterior.Height -= 6
                        If pnSubAnterior.Height = 1 Then
                            pnSubAnterior.Height -= 1
                        End If
                    ElseIf SizeClose = 132 Then
                        pnSubAnterior.Height -= 12
                    End If
                    pnSubAnterior.Refresh()
                    System.Threading.Thread.Sleep(5)
                End While
            End If

            'Animação Menu Principal
            '64 = Tamanho Width do panel do Menu Principal
            'pnProximo.Width cresce no mesmo ritmo que pnAnterior diminui
            While pnProximo.Width < 64
                pnProximo.Width = pnProximo.Width + 4
                pnAnterior.Width = pnAnterior.Width - 4
                pnProximo.Refresh()
                pnProximoAux.Refresh()
                pnAnterior.Refresh()
                pnAnteriorAux.Refresh()
            End While

            'Abrir SubMenu solicitado. PS: Home não tem Submenu
            'Caso Home, não executa
            If pnProximo.Name <> "pnHome" Then
                While pnSubProximo.Height < SizeOpen
                    If SizeOpen = 67 Then
                        pnSubProximo.Height += 6
                        If pnSubProximo.Height = (SizeOpen - 1) Then
                            pnSubProximo.Height += 1
                        End If
                    ElseIf SizeOpen = 132 Then
                        pnSubProximo.Height += 12
                        If pnSubProximo.Height = (2 * SizeOpen - 1) Then
                            pnSubProximo.Height += 1
                        End If
                    End If
                    pnSubProximo.Refresh()
                    System.Threading.Thread.Sleep(5)
                End While
            End If

            'Caso pnProximo seja a Home, troca a tela para logo maior

        End If
        If nivel = 2 Then

            'No nivel 2, o pnProximo é a Barra dos Topics
            pnLogo.Visible = False
            pnProximo.Visible = True
            pnToOpenScreen.Visible = True

        End If
    End Sub

    Public Function selectAtual() As Panel
        Dim panelResposta As Panel
        If homeAtivo Then
            panelResposta = pnHome
        ElseIf admAtivo Then
            panelResposta = pnAdm
        ElseIf cartaoAtivo Then
            panelResposta = pnCartao
        ElseIf vendAtivo Then
            panelResposta = pnVend
        ElseIf prodAtivo Then
            panelResposta = pnProd
        ElseIf relatAtivo Then
            panelResposta = pnRelat
        ElseIf infoAtivo Then
            panelResposta = pnInfo
        End If
        Return panelResposta
    End Function

    Public Sub alteraBoolMenu(pnAtivo As Panel)
        With pnAtivo
            homeAtivo = False
            admAtivo = False
            cartaoAtivo = False
            vendAtivo = False
            relatAtivo = False
            prodAtivo = False
            infoAtivo = False
            If .Name = "pnHome" Then
                homeAtivo = True
            ElseIf .Name = "pnAdm" Then
                admAtivo = True
            ElseIf .Name = "pnCartao" Then
                cartaoAtivo = True
            ElseIf .Name = "pnVend" Then
                vendAtivo = True
            ElseIf .Name = "pnRelat" Then
                relatAtivo = True
            ElseIf .Name = "pnProd" Then
                prodAtivo = True
            ElseIf .Name = "pnInfo" Then
                infoAtivo = True
            End If
        End With
    End Sub

    Public Sub FecharSubMenuScreenEBar(pnProximo As Panel)
        'Fecha Screens
        pnAdmAddScreen.Visible = False
        pnAdmDelScreen.Visible = False
        pnCartaoAddScreen.Visible = False
        pnCartaoDelScreen.Visible = False
        pnCartaoRecScreen.Visible = False
        pnCartaoDevScreen.Visible = False
        pnVendAddScreen.Visible = False
        pnVendDelScreen.Visible = False
        pnVendReqScreen.Visible = False
        pnVendOnScreen.Visible = False
        pnHomeTela.Visible = True
        'Fecha Bars Submenu
        pnAdmTopicAddBar.Visible = False
        pnAdmTopicDelBar.Visible = False
        pnCartaoTopicAddBar.Visible = False
        pnCartaoTopicDelBar.Visible = False
        pnCartaoTopicRecBar.Visible = False
        pnCartaoTopicDevBar.Visible = False
        pnVendTopicAddBar.Visible = False
        pnVendTopicDelBar.Visible = False
        pnVendTopicReqBar.Visible = False
        pnVendTopicOnBar.Visible = False
    End Sub


#End Region

#Region "Botoes Nivel1"
    Private Sub pbHomeAux_Click(sender As Object, e As EventArgs) Handles pbHomeAux.Click
        navegacaoMenu(1, pnHome, pnHomeTela)
        alteraBoolMenu(pnHome)
    End Sub
    Private Sub pbAdmAux_Click(sender As Object, e As EventArgs) Handles pbAdmAux.Click
        navegacaoMenu(1, pnAdm, pnLogo)
        alteraBoolMenu(pnAdm)
    End Sub

    Private Sub pbCartaoAux_Click(sender As Object, e As EventArgs) Handles pbCartaoAux.Click
        navegacaoMenu(1, pnCartao, pnLogo)
        alteraBoolMenu(pnCartao)
    End Sub
    Private Sub pbVendAux_Click(sender As Object, e As EventArgs) Handles pbVendAux.Click
        navegacaoMenu(1, pnVend, pnLogo)
        alteraBoolMenu(pnVend)
    End Sub

    Private Sub pbProdAux_Click(sender As Object, e As EventArgs) Handles pbProdAux.Click
        navegacaoMenu(1, pnProd, pnLogo)
        alteraBoolMenu(pnProd)
    End Sub
    Private Sub pbRelatAux_Click(sender As Object, e As EventArgs) Handles pbRelatAux.Click
        navegacaoMenu(1, pnRelat, pnLogo)
        alteraBoolMenu(pnRelat)
    End Sub

    Private Sub pbInfoAux_Click(sender As Object, e As EventArgs) Handles pbInfoAux.Click
        navegacaoMenu(1, pnInfo, pnLogo)
        alteraBoolMenu(pnInfo)
    End Sub




    Private Sub pnHome_Click(sender As Object, e As EventArgs) Handles pnHome.Click
        pbHomeAux_Click(sender, e)
    End Sub

    Private Sub pnAdmAux_Click(sender As Object, e As EventArgs) Handles pnAdmAux.Click
        pbAdmAux_Click(sender, e)
    End Sub

    Private Sub pnCartaoAux_Click(sender As Object, e As EventArgs) Handles pnCartaoAux.Click
        pbCartaoAux_Click(sender, e)
    End Sub


    Private Sub pnVendAux_Click(sender As Object, e As EventArgs) Handles pnVendAux.Click
        pbVendAux_Click(sender, e)
    End Sub

    Private Sub pnProdAux_Click(sender As Object, e As EventArgs) Handles pnProdAux.Click
        pbProdAux_Click(sender, e)
    End Sub

    Private Sub pnRelatAux_Click(sender As Object, e As EventArgs) Handles pnRelatAux.Click
        pbRelatAux_Click(sender, e)
    End Sub

    Private Sub pnInfoAux_Click(sender As Object, e As EventArgs) Handles pnInfoAux.Click
        pbInfoAux_Click(sender, e)
    End Sub

#End Region

#Region "Botoes Nivel2"

#Region "AdmTopics"
    Private Sub lbAdmTopicAdd_Click(sender As Object, e As EventArgs) Handles lbAdmTopicAdd.Click
        navegacaoMenu(2, pnAdmTopicAddBar, pnAdmAddScreen)
    End Sub
    Private Sub pnAddTopicAdd_Click(sender As Object, e As EventArgs) Handles pnAddTopicAdd.Click
        lbAdmTopicAdd_Click(sender, e)
    End Sub
    Private Sub lbAdmTopicDel_Click(sender As Object, e As EventArgs) Handles lbAdmTopicDel.Click
        navegacaoMenu(2, pnAdmTopicDelBar, pnAdmDelScreen)
    End Sub
    Private Sub pnAdmTopicDel_Click(sender As Object, e As EventArgs) Handles pnAdmTopicDel.Click
        lbAdmTopicDel_Click(sender, e)
    End Sub
#End Region

#Region "CartaoTopics"
    Private Sub lbCartaoTopicAdd_Click(sender As Object, e As EventArgs) Handles lbCartaoTopicAdd.Click
        navegacaoMenu(2, pnCartaoTopicAddBar, pnCartaoAddScreen)
    End Sub

    Private Sub pnCartaoTopicAdd_Click(sender As Object, e As EventArgs) Handles pnCartaoTopicAdd.Click
        lbCartaoTopicAdd_Click(sender, e)
    End Sub

    Private Sub lbCartaoTopicDel_Click(sender As Object, e As EventArgs) Handles lbCartaoTopicDel.Click
        navegacaoMenu(2, pnCartaoTopicDelBar, pnCartaoDelScreen)
    End Sub
    Private Sub pnCartaoTopicDel_Click(sender As Object, e As EventArgs) Handles pnCartaoTopicDel.Click
        lbCartaoTopicDel_Click(sender, e)
    End Sub

    Private Sub lbCartaoTopicRec_Click(sender As Object, e As EventArgs) Handles lbCartaoTopicRec.Click
        navegacaoMenu(2, pnCartaoTopicRecBar, pnCartaoRecScreen)
    End Sub

    Private Sub pnCartaoTopicRec_Click(sender As Object, e As EventArgs) Handles pnCartaoTopicRec.Click
        lbCartaoTopicRec_Click(sender, e)
    End Sub

    Private Sub lbCartaoTopicDev_Click(sender As Object, e As EventArgs) Handles lbCartaoTopicDev.Click
        navegacaoMenu(2, pnCartaoTopicDevBar, pnCartaoDevScreen)
    End Sub

    Private Sub pnCartaoTopicDev_Click(sender As Object, e As EventArgs) Handles pnCartaoTopicDev.Click
        lbCartaoTopicDev_Click(sender, e)
    End Sub
#End Region

#Region "VendedorTopics"
    Private Sub lbVendTopicAdd_Click(sender As Object, e As EventArgs) Handles lbVendTopicAdd.Click
        navegacaoMenu(2, pnVendTopicAddBar, pnVendAddScreen)
    End Sub

    Private Sub Panel32_Paint(sender As Object, e As EventArgs) Handles pnVendTopicAdd.Click
        lbVendTopicAdd_Click(sender, e)
    End Sub

    Private Sub pnVendTopicDel_CLick(sender As Object, e As EventArgs) Handles pnVendTopicDel.Click
        lbVendTopicDel_Click(sender, e)
    End Sub

    Private Sub lbVendTopicDel_Click(sender As Object, e As EventArgs) Handles lbVendTopicDel.Click
        navegacaoMenu(2, pnVendTopicDelBar, pnVendDelScreen)
    End Sub

    Private Sub lbVendTopicReq_Click(sender As Object, e As EventArgs) Handles lbVendTopicReq.Click
        navegacaoMenu(2, pnVendTopicReqBar, pnVendReqScreen)
    End Sub

    Private Sub pnVendTopicReq_Click(sender As Object, e As EventArgs) Handles pnVendTopicReq.Click
        lbVendTopicReq_Click(sender, e)
    End Sub

    Private Sub lbVendTopicOn_Click(sender As Object, e As EventArgs) Handles lbVendTopicOn.Click
        navegacaoMenu(2, pnVendTopicOnBar, pnVendReqScreen)
    End Sub

    Private Sub pnVendTopicOn_Click(sender As Object, e As EventArgs) Handles pnVendTopicOn.Click
        lbVendTopicOn_Click(sender, e)
    End Sub
#End Region

#End Region

#End Region

#Region "Conexao com a API"

#Region "Adm"

    'Adicionar ADM
    Private Async Sub btAdmAddSalvar_Click(sender As Object, e As EventArgs) Handles btAdmAddSalvar.Click
        Dim nome As String = tbAdmAddNome.Text
        Dim login As String = tbAdmAddLogin.Text
        Dim senha As String = cripto(tbAdmAddSenha.Text, 1, 1)
        Dim email As String = tbAdmAddEmail.Text
        If (nome <> Nothing And login <> Nothing And senha <> Nothing And email <> Nothing) Then
            'formMsgBox.chamadaMSG("Confirme os dados: " & "" & vbNewLine & "" & "Nome: " & nome & "" & vbNewLine & "" & "Login: " & login & "" & vbNewLine & "" & "E-mail: " & email, Color.White, 3)
            'Await 
            'MsgBox(formMsgBox.resposta.ToString)
            'If formMsgBox.resposta Then
            addAdmin(nome, login, senha, email)
            'Else
            'btAdmAddLimpar_Click(Nothing, Nothing)
            'End If
        Else
            formMsgBox.chamadaMSG("Preencha todos os campos " & "" & vbNewLine & "" & "antes de salvar!", 1)
        End If
    End Sub

    'Busca ADM para exclusão
    Private Sub btAdmDelBusca_Click(sender As Object, e As EventArgs) Handles btAdmDelBusca.Click
        excAdmin(tbAdmDelBusca.Text)
    End Sub

    'ApagaADM
    Private Sub btAdmDelApagar_Click(sender As Object, e As EventArgs) Handles btAdmDelApagar.Click
        Dim myUri As String
        If cbAdmDellBy.Text = "Login" Then
            myUri = "https://sistemaifrj.herokuapp.com/admins/l/"
        ElseIf cbAdmDellBy.Text = "E-mail" Then
            myUri = "https://sistemaifrj.herokuapp.com/admins/e/"
        End If
        If MsgBox("Deseja prosseguir com a exclusão do Administrador " & tbAdmDelNome.Text & "?", vbYesNo) = MsgBoxResult.Yes Then
            delete(tbAdmDelBusca.Text, myUri)
        Else
            btAdmDelCancelar_Click(sender, e)
        End If
        If sucessDelete Then
            tbAdmDelBusca.Clear()
            tbAdmDelEmail.Clear()
            tbAdmDelLogin.Clear()
            tbAdmDelNome.Clear()
        End If
    End Sub

#End Region

#Region "Cartao"

    Dim totalRecarga As Integer
    'Adicionar Cartao
    Private Sub btCartaoAddSalvar_Click(sender As Object, e As EventArgs) Handles btCartaoAddSalvar.Click
        Dim matricula As String = tbCartaoAddMat.Text
        Dim nome As String = tbCartaoAddNome.Text
        Dim email As String = tbCartaoAddEmail.Text
        Dim saldo As String = tbCartaoAddSaldo.Text
        If matricula <> Nothing And nome <> Nothing And email <> Nothing And saldo <> Nothing Then
            addCartao(matricula, nome, email, saldo)
        Else
            formMsgBox.chamadaMSG("Preencha todos os campos " & "" & vbNewLine & "" & "antes de salvar!", 1)
            formMsgBox.Show()
            Me.Enabled = False
        End If
    End Sub

    'Buscar Cartao para exclusão
    Private Sub btCartaoDelBusca_Click(sender As Object, e As EventArgs) Handles btCartaoDelBusca.Click
        excCartao(tbCartaoDelBusca.Text)
    End Sub

    Private Sub btCartaoDelApagar_Click(sender As Object, e As EventArgs) Handles btCartaoDelApagar.Click
        Dim myUri As String = "https://sistemaifrj.herokuapp.com/users/"
        delete(tbCartaoDelBusca.Text, myUri)
    End Sub

    'Buscar Cartão para recarga
    Private Sub btCartaoReqBusca_Click(sender As Object, e As EventArgs) Handles btCartaoReqBusca.Click
        recCartao(tbCartaoRecMat.Text)
    End Sub

    Private Sub btCartaoRec5_Click(sender As Object, e As EventArgs) Handles btCartaoRec5.Click
        totalRecarga += 5 * 100
        tbCartaoRecTotalRecarga.Text = "R$ " & totalRecarga.ToString
    End Sub

    Private Sub btCartaoRec10_Click(sender As Object, e As EventArgs) Handles btCartaoRec10.Click
        totalRecarga += 10 * 100
        tbCartaoRecTotalRecarga.Text = "R$ " & totalRecarga.ToString
    End Sub
    Private Sub btCartaoRec20_Click(sender As Object, e As EventArgs) Handles btCartaoRec20.Click
        totalRecarga += 20 * 100
        tbCartaoRecTotalRecarga.Text = "R$ " & totalRecarga.ToString
    End Sub

    Private Sub btCartaoRec50_Click(sender As Object, e As EventArgs) Handles btCartaoRec50.Click
        totalRecarga += 50 * 100
        tbCartaoRecTotalRecarga.Text = "R$ " & totalRecarga.ToString
    End Sub

    Private Sub btCartaoRec100_Click(sender As Object, e As EventArgs) Handles btCartaoRec100.Click
        totalRecarga += 100 * 100
        tbCartaoRecTotalRecarga.Text = "R$ " & totalRecarga.ToString
    End Sub

    Private Sub btCartaoRecOutro_Click(sender As Object, e As EventArgs) Handles btCartaoRecOutro.Click
        tbCartaoRecTotalRecarga.Text = "R$"
        While pnCartaoRecOutroValor.Width < 356
            pnCartaoRecOutroValor.Width += 3
            If pnCartaoRecOutroValor.Width = 354 Then
                pnCartaoRecOutroValor.Width += 2
            End If
            pnCartaoRecOutroValor.Refresh()
        End While
    End Sub
    Private Sub pbCartaoRecLimpaTotalRecarga_Click(sender As Object, e As EventArgs) Handles pbCartaoRecLimpaTotalRecarga.Click
        totalRecarga = 0
        tbCartaoRecTotalRecarga.Text = "R$ " & totalRecarga.ToString
        lbCartaoRecSaldoTt.Text = lbCartaoRecSaldoAt.Text
    End Sub

    Private Sub tbCartaoRecOutroValor_TextChanged(sender As Object, e As EventArgs) Handles tbCartaoRecOutroValor.TextChanged
        moeda(tbCartaoRecOutroValor)
    End Sub

    Private Sub btCartaoRecOutroValo_Click(sender As Object, e As EventArgs) Handles btCartaoRecOutroValorOK.Click
        tbCartaoRecTotalRecarga.Text = "R$ " & tbCartaoRecOutroValor.Text
        While pnCartaoRecOutroValor.Width > 0
            pnCartaoRecOutroValor.Width -= 3
            pnCartaoRecOutroValor.Refresh()
        End While
    End Sub
    Private Sub btCartaoRecOutroValoCancelar_Click(sender As Object, e As EventArgs) Handles btCartaoRecOutroValoCancelar.Click
        While pnCartaoRecOutroValor.Width > 0
            pnCartaoRecOutroValor.Width -= 3
            pnCartaoRecOutroValor.Refresh()
        End While
    End Sub

    Private Sub tbCartaoRecTotalRecarga_TextChanged(sender As Object, e As EventArgs) Handles tbCartaoRecTotalRecarga.TextChanged
        Dim brutalString, brutalString2 As String
        brutalString = Replace(Replace(Replace(Replace(Replace(tbCartaoRecTotalRecarga.Text, "R", ""), "$", ""), " ", ""), ".", ""), ",", "")
        brutalString2 = Replace(Replace(Replace(Replace(Replace(lbCartaoRecSaldoTt.Text, "R", ""), "$", ""), " ", ""), ".", ""), ",", "")
        lbCartaoRecSaldoTt.Text = Val(brutalString) + Val(brutalString2)
    End Sub

    Private Sub btCartaoRecConfirmar_Click(sender As Object, e As EventArgs) Handles btCartaoRecConfirmar.Click
        If (cbCartaoRecModoPG.Text <> "Cartão" And cbCartaoRecModoPG.Text <> "Dinheiro") Then
            MsgBox("Selecione um modo de pagamento válido")
        Else
            exeRecarga(cbCartaoRecModoPG.Text, Val(lbCartaoRecSaldoTt.Text))
        End If

    End Sub

    Public Shared Sub moeda(ByRef txt As MaskedTextBox)
        Dim n As String = String.Empty
        Dim v As Double = 0
        n = txt.Text.Replace(",", "").Replace(".", "")
        If n.Equals("") Then n = ""
        If n.Length > 3 And n.Substring(0, 1) = "0" Then n = n.Substring(1, n.Length - 1)
        v = Convert.ToDouble(n) / 100
        txt.Text = String.Format("{0:N}", v)

        txt.SelectionStart = txt.Text.Length

    End Sub


#End Region

#Region "Vendedor"

    'Adicionar Vendedor
    Private Sub btVendAddSalvar_Click(sender As Object, e As EventArgs) Handles btVendAddSalvar.Click
        Dim matricula As String = tbVendAddMat.Text
        Dim nome As String = tbVendAddNome.Text
        Dim email As String = tbVendAddEmail.Text
        Dim senha As String = cripto(tbVendAddSenha.Text, 1, 1)
        If matricula <> Nothing And nome <> Nothing And email <> Nothing And senha <> Nothing Then
            addVend(matricula, nome, email, senha)
        Else
            With formMsgBox
                .chamadaMSG("Preencha todos os campos " & "" & vbNewLine & "" & "antes de salvar!", 1)
                .Show()
                '.formMsgBox_LoadEffect(sender, e)
            End With
            Me.Enabled = False
        End If

    End Sub

    Private Sub btVendDelBusca_Click(sender As Object, e As EventArgs) Handles btVendDelBusca.Click
        'excVendedor(tbVendDelBusca.Text & "/")
    End Sub

#End Region

#Region "Produto"

#End Region

#Region "Relatorio"
#End Region

#End Region

#Region "Bright Mode"

    Private Sub pbBrightMode_Click(sender As Object, e As EventArgs) Handles pbBrightMode.Click
        If Not brightMode Then
            brightMode = True
            pbBrightMode.Image = My.Resources.imgMoonIcon
        Else
            brightMode = False
            pbBrightMode.Image = My.Resources.imgSunIcon
        End If
        'alteraDesign()
    End Sub



    Private Sub pnBrightMode_Click(sender As Object, e As EventArgs) Handles pnBrightMode.Click
        pbBrightMode_Click(sender, e)
    End Sub


    '    Public Sub alteraDesign()

    '#Region "Modo Claro"
    '        If brightMode Then

    '            'Home/FundoSubMenu/LogoCor/BotõesFecharMinimizar

    '            Me.BackColor = Color.White
    '            pbMinimizar.Image = My.Resources.imgMinimizarIconCinza
    '            pbFechar.Image = My.Resources.imgFecharCinza
    '            pbHomeLogo.Image = My.Resources.imgLogoCirculoPreto
    '            pbLogo.Image = My.Resources.imgLogoCirculoPreto
    '            pnMenu.BackColor = Color.White
    '            pnBarraSuperior.BackColor = Color.White
    '            pnLogo.BackColor = Color.White
    '            pnHomeTela.BackColor = Color.White
    '            pnBrightMode.BackColor = Color.White
    '            pnSubMenuAdm.BackColor = Color.FromArgb(120, 120, 120)
    '            pnSubMenuCartao.BackColor = Color.FromArgb(120, 120, 120)


    '            'PanelMenuLateralOpções

    '            pnHome.BackColor = Color.White
    '            pnAdm.BackColor = Color.White
    '            pnCartao.BackColor = Color.White
    '            pnVend.BackColor = Color.White
    '            pnRelat.BackColor = Color.White
    '            pnProd.BackColor = Color.White
    '            pnInfo.BackColor = Color.White
    '            pbHome.Image = My.Resources.imgHomeIconCinza
    '            pbAdm.Image = My.Resources.imgAdmIconCinza
    '            pbCartao.Image = My.Resources.imgCartaoIconCinza
    '            pbVend.Image = My.Resources.imgVendedorIconCinza
    '            pbRelat.Image = My.Resources.imgRelatorioIconCinza
    '            pbProd.Image = My.Resources.imgProdutoIconCinza
    '            pbInfo.Image = My.Resources.imgInfoIconCinza
    '            If homeAtivo Then
    '                pnHome.BackColor = Color.FromArgb(120, 120, 120)
    '                pbHome.Image = My.Resources.imgHomeIconPreto
    '            ElseIf admAtivo Then
    '                pnAdm.BackColor = Color.FromArgb(120, 120, 120)
    '                pbAdm.Image = My.Resources.imgAdmIconPreto
    '            ElseIf cartaoAtivo Then
    '                pnCartao.BackColor = Color.FromArgb(120, 120, 120)
    '                pbCartao.Image = My.Resources.imgCartaoIconPreto
    '            ElseIf vendAtivo Then
    '                pnVend.BackColor = Color.FromArgb(120, 120, 120)
    '                pbVend.Image = My.Resources.imgVendedorIconPreto
    '            ElseIf relatAtivo Then
    '                pnRelat.BackColor = Color.FromArgb(120, 120, 120)
    '                pbRelat.Image = My.Resources.imgRelatorioIconPreto
    '            ElseIf prodAtivo Then
    '                pnProd.BackColor = Color.FromArgb(120, 120, 120)
    '                pbProd.Image = My.Resources.imgProdutoIconPreto

    '            ElseIf infoAtivo Then
    '                pnInfo.BackColor = Color.FromArgb(120, 120, 120)
    '                pbInfo.Image = My.Resources.imgInfoIconPreto
    '            End If


    '            'Screens

    '            pnAdmAddScreen.BackColor = Color.White
    '            pnAdmDelScreen.BackColor = Color.White
    '            pnCartaoAddScreen.BackColor = Color.White
    '            pnCartaoDelScreen.BackColor = Color.White
    '            pnCartaoDevScreen.BackColor = Color.White
    '            pnCartaoRecScreen.BackColor = Color.White


    '            'Botões

    '            btAdmAddSalvar.ForeColor = Color.White
    '            btAdmDelApagar.ForeColor = Color.White
    '            btAdmDelBusca.ForeColor = Color.White
    '            btCartaoAddSalvar.ForeColor = Color.White
    '            btCartaoDelBusca.ForeColor = Color.White
    '            btCartaoDelApagar.ForeColor = Color.White
    '            btCartaoDevBusca.ForeColor = Color.White
    '            btCartaoDevDevolver.ForeColor = Color.White
    '            btAdmAddSalvar.BackColor = Color.FromArgb(32, 32, 32)
    '            btAdmDelApagar.BackColor = Color.FromArgb(32, 32, 32)
    '            btAdmDelBusca.BackColor = Color.FromArgb(32, 32, 32)
    '            btCartaoAddSalvar.BackColor = Color.FromArgb(32, 32, 32)
    '            btCartaoDelBusca.BackColor = Color.FromArgb(32, 32, 32)
    '            btCartaoDelApagar.BackColor = Color.FromArgb(32, 32, 32)
    '            btCartaoDevBusca.BackColor = Color.FromArgb(32, 32, 32)
    '            btCartaoDevDevolver.BackColor = Color.FromArgb(32, 32, 32)
    '            btAdmAddLimpar.BackColor = Color.FromArgb(120, 120, 120)
    '            btCartaoAddLimpar.BackColor = Color.FromArgb(120, 120, 120)
    '            btAdmDelCancelar.BackColor = Color.FromArgb(120, 120, 120)


    '            'Tópico SubMenu Bar

    '            pnAdmTopicAddBar.BackColor = Color.FromArgb(120, 120, 120)
    '            pnAdmTopicDelBar.BackColor = Color.FromArgb(120, 120, 120)
    '            pnCartaoTopicAddBar.BackColor = Color.FromArgb(120, 120, 120)
    '            pnCartaoTopicDelBar.BackColor = Color.FromArgb(120, 120, 120)
    '            pnCartaoTopicRecBar.BackColor = Color.FromArgb(120, 120, 120)
    '            pnCartaoTopicDevBar.BackColor = Color.FromArgb(120, 120, 120)
    '            If pnLogo.Visible Then

    '            ElseIf pnAdmAddScreen.Visible Then
    '                pnAdmTopicAddBar.BackColor = Color.FromArgb(32, 32, 32)
    '            ElseIf pnAdmDelScreen.Visible Then
    '                pnAdmTopicDelBar.BackColor = Color.FromArgb(32, 32, 32)
    '            ElseIf pnCartaoAddScreen.Visible Then
    '                pnCartaoTopicAddBar.BackColor = Color.FromArgb(32, 32, 32)
    '            ElseIf pnCartaoDelScreen.Visible Then
    '                pnCartaoTopicDelBar.BackColor = Color.FromArgb(32, 32, 32)
    '            ElseIf pnCartaoRecScreen.Visible Then
    '                pnCartaoTopicRecBar.BackColor = Color.FromArgb(32, 32, 32)
    '            ElseIf pnCartaoDevScreen.Visible Then
    '                pnCartaoTopicDevBar.BackColor = Color.FromArgb(32, 32, 32)
    '            End If


    '            'Labels/TextBoxs

    '            lbAdmTopicAdd.ForeColor = Color.FromArgb(32, 32, 32)
    '            lbAdmTopicDel.ForeColor = Color.FromArgb(32, 32, 32)
    '            lbCartaoTopicAdd.ForeColor = Color.FromArgb(32, 32, 32)
    '            lbCartaoTopicDel.ForeColor = Color.FromArgb(32, 32, 32)
    '            lbCartaoTopicRec.ForeColor = Color.FromArgb(32, 32, 32)
    '            lbCartaoTopicDev.ForeColor = Color.FromArgb(32, 32, 32)
    '            Label1.ForeColor = Color.FromArgb(32, 32, 32)
    '            Label2.ForeColor = Color.FromArgb(32, 32, 32)
    '            Label3.ForeColor = Color.FromArgb(32, 32, 32)
    '            Label4.ForeColor = Color.FromArgb(32, 32, 32)
    '            Label5.ForeColor = Color.FromArgb(32, 32, 32)
    '            Label6.ForeColor = Color.FromArgb(32, 32, 32)

    '            'Cor da Fonte'FundoTB
    '            tbAdmAddEmail.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbAdmAddLogin.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbAdmDelLogin.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbAdmAddNome.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbAdmAddSenha.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbAdmDelBusca.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbAdmDelLogin.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbAdmDelEmail.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbAdmDelNome.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoAddEmail.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoAddMat.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoAddNome.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoAddSaldo.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDelBusca.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDelEmail.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDelNome.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDelSaldo.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDevBusca.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDevData.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDevModo.ForeColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDevValor.ForeColor = Color.FromArgb(32, 32, 32)
    '            TextBox5.ForeColor = Color.FromArgb(32, 32, 32)
    '            TextBox6.ForeColor = Color.FromArgb(32, 32, 32)
    '            TextBox7.ForeColor = Color.FromArgb(32, 32, 32)
    '            TextBox8.ForeColor = Color.FromArgb(32, 32, 32)

    '            'FundoTB
    '            tbAdmAddEmail.BackColor = Color.White
    '            tbAdmAddLogin.BackColor = Color.White
    '            tbAdmDelLogin.BackColor = Color.White
    '            tbAdmAddNome.BackColor = Color.White
    '            tbAdmAddSenha.BackColor = Color.White
    '            tbAdmDelBusca.BackColor = Color.White
    '            tbAdmDelLogin.BackColor = Color.White
    '            tbAdmDelEmail.BackColor = Color.White
    '            tbAdmDelNome.BackColor = Color.White
    '            tbCartaoAddEmail.BackColor = Color.White
    '            tbCartaoAddMat.BackColor = Color.White
    '            tbCartaoAddNome.BackColor = Color.White
    '            tbCartaoAddSaldo.BackColor = Color.White
    '            tbCartaoDelBusca.BackColor = Color.White
    '            tbCartaoDelEmail.BackColor = Color.White
    '            tbCartaoDelNome.BackColor = Color.White
    '            tbCartaoDelSaldo.BackColor = Color.White
    '            tbCartaoDevBusca.BackColor = Color.White
    '            tbCartaoDevData.BackColor = Color.White
    '            tbCartaoDevModo.BackColor = Color.White
    '            tbCartaoDevValor.BackColor = Color.White
    '            TextBox5.BackColor = Color.White
    '            TextBox6.BackColor = Color.White
    '            TextBox7.BackColor = Color.White
    '            TextBox8.BackColor = Color.White


    '            'IconsTextBoxs

    '            'PanelAux

    '            FlowLayoutPanel1.BackColor = Color.FromArgb(32, 32, 32)
    '            FlowLayoutPanel10.BackColor = Color.FromArgb(32, 32, 32)
    '            FlowLayoutPanel11.BackColor = Color.FromArgb(32, 32, 32)
    '            FlowLayoutPanel11.BackColor = Color.FromArgb(32, 32, 32)
    '            FlowLayoutPanel12.BackColor = Color.FromArgb(32, 32, 32)
    '            FlowLayoutPanel2.BackColor = Color.FromArgb(32, 32, 32)
    '            FlowLayoutPanel3.BackColor = Color.FromArgb(32, 32, 32)
    '            FlowLayoutPanel4.BackColor = Color.FromArgb(32, 32, 32)
    '            FlowLayoutPanel9.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel1.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel10.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel11.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel12.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel13.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel14.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel15.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel16.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel2.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel3.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel4.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel5.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel6.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel7.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel8.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel9.BackColor = Color.FromArgb(32, 32, 32)
    '            Panel17.BackColor = Color.FromArgb(120, 120, 120)
    '            Panel18.BackColor = Color.FromArgb(120, 120, 120)
    '            Panel19.BackColor = Color.FromArgb(120, 120, 120)
    '            Panel20.BackColor = Color.FromArgb(120, 120, 120)
    '            Panel21.BackColor = Color.FromArgb(120, 120, 120)
    '            Panel22.BackColor = Color.FromArgb(120, 120, 120)
    '            Panel23.BackColor = Color.FromArgb(120, 120, 120)
    '            Panel24.BackColor = Color.FromArgb(120, 120, 120)
    '#End Region

    '#Region "Modo Escuro"
    '        Else

    '            'HomeTela/FundoSubMenu/LogoCor/BotõesFecharMinimizar

    '            Me.BackColor = Color.FromArgb(32, 32, 32)
    '            pbMinimizar.Image = My.Resources.imgMinimizarIconBranco
    '            pbFechar.Image = My.Resources.imgFecharIconBranco
    '            pbHomeLogo.Image = My.Resources.imgLogoCirculo
    '            pbLogo.Image = My.Resources.imgLogoCirculo
    '            pnMenu.BackColor = Color.FromArgb(32, 32, 32)
    '            pnBarraSuperior.BackColor = Color.FromArgb(32, 32, 32)
    '            pnLogo.BackColor = Color.FromArgb(32, 32, 32)
    '            pnHomeTela.BackColor = Color.FromArgb(32, 32, 32)
    '            pnBrightMode.BackColor = Color.FromArgb(32, 32, 32)
    '            pnSubMenuAdm.BackColor = Color.FromArgb(56, 56, 56)
    '            pnSubMenuCartao.BackColor = Color.FromArgb(56, 56, 56)


    '            'PanelMenuLateralOpções

    '            pnHome.BackColor = Color.FromArgb(32, 32, 32)
    '            pnAdm.BackColor = Color.FromArgb(32, 32, 32)
    '            pnCartao.BackColor = Color.FromArgb(32, 32, 32)
    '            pnVend.BackColor = Color.FromArgb(32, 32, 32)
    '            pnRelat.BackColor = Color.FromArgb(32, 32, 32)
    '            pnProd.BackColor = Color.FromArgb(32, 32, 32)
    '            pnInfo.BackColor = Color.FromArgb(32, 32, 32)
    '            pbHome.Image = My.Resources.imgHomeIconPreto
    '            pbAdm.Image = My.Resources.imgAdmIconPreto
    '            pbCartao.Image = My.Resources.imgCartaoIconPreto
    '            pbVend.Image = My.Resources.imgVendedorIconPreto
    '            pbRelat.Image = My.Resources.imgRelatorioIconPreto
    '            pbProd.Image = My.Resources.imgProdutoIconPreto
    '            pbInfo.Image = My.Resources.imgInfoIconPreto
    '            If homeAtivo Then
    '                pnHome.BackColor = Color.FromArgb(56, 56, 56)
    '                pbHome.Image = My.Resources.imgHomeIconBranco
    '            ElseIf admAtivo Then
    '                pnAdm.BackColor = Color.FromArgb(56, 56, 56)
    '                pbAdm.Image = My.Resources.imgAdmIconBranco
    '            ElseIf cartaoAtivo Then
    '                pnCartao.BackColor = Color.FromArgb(56, 56, 56)
    '                pbCartao.Image = My.Resources.imgCartaoIconBranco
    '            ElseIf vendAtivo Then
    '                pnVend.BackColor = Color.FromArgb(56, 56, 56)
    '                pbVend.Image = My.Resources.imgVendedorIconBranco
    '            ElseIf relatAtivo Then
    '                pnRelat.BackColor = Color.FromArgb(56, 56, 56)
    '                pbRelat.Image = My.Resources.imgRelatorioIconBranco
    '            ElseIf prodAtivo Then
    '                pnProd.BackColor = Color.FromArgb(56, 56, 56)
    '            ElseIf infoAtivo Then
    '                pnInfo.BackColor = Color.FromArgb(56, 56, 56)
    '                pbInfo.Image = My.Resources.imgInfoIconBranco
    '            End If


    '            'Screens

    '            pnAdmAddScreen.BackColor = Color.FromArgb(32, 32, 32)
    '            pnAdmDelScreen.BackColor = Color.FromArgb(32, 32, 32)
    '            pnCartaoAddScreen.BackColor = Color.FromArgb(32, 32, 32)
    '            pnCartaoDelScreen.BackColor = Color.FromArgb(32, 32, 32)
    '            pnCartaoDevScreen.BackColor = Color.FromArgb(32, 32, 32)
    '            pnCartaoRecScreen.BackColor = Color.FromArgb(32, 32, 32)


    '            'Botões

    '            btAdmAddSalvar.ForeColor = Color.FromArgb(32, 32, 32)
    '            btAdmDelApagar.ForeColor = Color.FromArgb(32, 32, 32)
    '            btAdmDelBusca.ForeColor = Color.FromArgb(32, 32, 32)
    '            btCartaoAddSalvar.ForeColor = Color.FromArgb(32, 32, 32)
    '            btCartaoDelBusca.ForeColor = Color.FromArgb(32, 32, 32)
    '            btCartaoDelApagar.ForeColor = Color.FromArgb(32, 32, 32)
    '            btCartaoDevBusca.ForeColor = Color.FromArgb(32, 32, 32)
    '            btCartaoDevDevolver.ForeColor = Color.FromArgb(32, 32, 32)
    '            btAdmAddSalvar.BackColor = Color.White
    '            btAdmDelApagar.BackColor = Color.White
    '            btAdmDelBusca.BackColor = Color.White
    '            btCartaoAddSalvar.BackColor = Color.White
    '            btCartaoDelBusca.BackColor = Color.White
    '            btCartaoDelApagar.BackColor = Color.White
    '            btCartaoDevBusca.BackColor = Color.White
    '            btCartaoDevDevolver.BackColor = Color.White
    '            btAdmAddLimpar.BackColor = Color.FromArgb(56, 56, 56)
    '            btCartaoAddLimpar.BackColor = Color.FromArgb(56, 56, 56)
    '            btAdmDelCancelar.BackColor = Color.FromArgb(56, 56, 56)


    '            'Tópico SubMenu Bar

    '            pnAdmTopicAddBar.BackColor = Color.FromArgb(56, 56, 56)
    '            pnAdmTopicDelBar.BackColor = Color.FromArgb(56, 56, 56)
    '            pnCartaoTopicAddBar.BackColor = Color.FromArgb(56, 56, 56)
    '            pnCartaoTopicDelBar.BackColor = Color.FromArgb(56, 56, 56)
    '            pnCartaoTopicRecBar.BackColor = Color.FromArgb(56, 56, 56)
    '            pnCartaoTopicDevBar.BackColor = Color.FromArgb(56, 56, 56)
    '            If pnAdmAddScreen.Visible Then
    '                pnAdmTopicAddBar.BackColor = Color.White
    '            ElseIf pnAdmDelScreen.Visible Then
    '                pnAdmTopicDelBar.BackColor = Color.White
    '            ElseIf pnCartaoAddScreen.Visible Then
    '                pnCartaoTopicAddBar.BackColor = Color.White
    '            ElseIf pnCartaoDelScreen.Visible Then
    '                pnCartaoTopicDelBar.BackColor = Color.White
    '            ElseIf pnCartaoRecScreen.Visible Then
    '                pnCartaoTopicRecBar.BackColor = Color.White
    '            ElseIf pnCartaoDevScreen.Visible Then
    '                pnCartaoTopicDevBar.BackColor = Color.White
    '            End If


    '            'Labels/TextBoxs
    '            lbAdmTopicAdd.ForeColor = Color.White
    '            lbAdmTopicDel.ForeColor = Color.White
    '            lbCartaoTopicAdd.ForeColor = Color.White
    '            lbCartaoTopicDel.ForeColor = Color.White
    '            lbCartaoTopicRec.ForeColor = Color.White
    '            lbCartaoTopicDev.ForeColor = Color.White
    '            Label1.ForeColor = Color.White
    '            Label2.ForeColor = Color.White
    '            Label3.ForeColor = Color.White
    '            Label4.ForeColor = Color.White
    '            Label5.ForeColor = Color.White
    '            Label6.ForeColor = Color.White

    '            'Cor da FonteTB
    '            tbAdmAddEmail.ForeColor = Color.White
    '            tbAdmAddLogin.ForeColor = Color.White
    '            tbAdmDelLogin.ForeColor = Color.White
    '            tbAdmAddNome.ForeColor = Color.White
    '            tbAdmAddSenha.ForeColor = Color.White
    '            tbAdmDelBusca.ForeColor = Color.White
    '            tbAdmDelLogin.ForeColor = Color.White
    '            tbAdmDelEmail.ForeColor = Color.White
    '            tbAdmDelNome.ForeColor = Color.White
    '            tbCartaoAddEmail.ForeColor = Color.White
    '            tbCartaoAddMat.ForeColor = Color.White
    '            tbCartaoAddNome.ForeColor = Color.White
    '            tbCartaoAddSaldo.ForeColor = Color.White
    '            tbCartaoDelBusca.ForeColor = Color.White
    '            tbCartaoDelEmail.ForeColor = Color.White
    '            tbCartaoDelNome.ForeColor = Color.White
    '            tbCartaoDelSaldo.ForeColor = Color.White
    '            tbCartaoDevBusca.ForeColor = Color.White
    '            tbCartaoDevData.ForeColor = Color.White
    '            tbCartaoDevModo.ForeColor = Color.White
    '            tbCartaoDevValor.ForeColor = Color.White
    '            TextBox5.ForeColor = Color.White
    '            TextBox6.ForeColor = Color.White
    '            TextBox7.ForeColor = Color.White
    '            TextBox8.ForeColor = Color.White

    '            'FundoTB
    '            tbAdmAddEmail.BackColor = Color.FromArgb(32, 32, 32)
    '            tbAdmAddLogin.BackColor = Color.FromArgb(32, 32, 32)
    '            tbAdmDelLogin.BackColor = Color.FromArgb(32, 32, 32)
    '            tbAdmAddNome.BackColor = Color.FromArgb(32, 32, 32)
    '            tbAdmAddSenha.BackColor = Color.FromArgb(32, 32, 32)
    '            tbAdmDelBusca.BackColor = Color.FromArgb(32, 32, 32)
    '            tbAdmDelLogin.BackColor = Color.FromArgb(32, 32, 32)
    '            tbAdmDelEmail.BackColor = Color.FromArgb(32, 32, 32)
    '            tbAdmDelNome.BackColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoAddEmail.BackColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoAddMat.BackColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoAddNome.BackColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoAddSaldo.BackColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDelBusca.BackColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDelEmail.BackColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDelNome.BackColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDelSaldo.BackColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDevBusca.BackColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDevData.BackColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDevModo.BackColor = Color.FromArgb(32, 32, 32)
    '            tbCartaoDevValor.BackColor = Color.FromArgb(32, 32, 32)
    '            TextBox5.BackColor = Color.FromArgb(32, 32, 32)
    '            TextBox6.BackColor = Color.FromArgb(32, 32, 32)
    '            TextBox7.BackColor = Color.FromArgb(32, 32, 32)
    '            TextBox8.BackColor = Color.FromArgb(32, 32, 32)


    '            'IconesTextBox

    '            'PanelAuxBar

    '            FlowLayoutPanel1.BackColor = Color.White
    '            FlowLayoutPanel10.BackColor = Color.White
    '            FlowLayoutPanel11.BackColor = Color.White
    '            FlowLayoutPanel11.BackColor = Color.White
    '            FlowLayoutPanel12.BackColor = Color.White
    '            FlowLayoutPanel2.BackColor = Color.White
    '            FlowLayoutPanel3.BackColor = Color.White
    '            FlowLayoutPanel4.BackColor = Color.White
    '            FlowLayoutPanel9.BackColor = Color.White
    '            Panel1.BackColor = Color.White
    '            Panel10.BackColor = Color.White
    '            Panel11.BackColor = Color.White
    '            Panel12.BackColor = Color.White
    '            Panel13.BackColor = Color.White
    '            Panel14.BackColor = Color.White
    '            Panel15.BackColor = Color.White
    '            Panel16.BackColor = Color.White
    '            Panel2.BackColor = Color.White
    '            Panel3.BackColor = Color.White
    '            Panel4.BackColor = Color.White
    '            Panel5.BackColor = Color.White
    '            Panel6.BackColor = Color.White
    '            Panel7.BackColor = Color.White
    '            Panel8.BackColor = Color.White
    '            Panel9.BackColor = Color.White
    '            Panel17.BackColor = Color.FromArgb(56, 56, 56)
    '            Panel18.BackColor = Color.FromArgb(56, 56, 56)
    '            Panel19.BackColor = Color.FromArgb(56, 56, 56)
    '            Panel20.BackColor = Color.FromArgb(56, 56, 56)
    '            Panel21.BackColor = Color.FromArgb(56, 56, 56)
    '            Panel22.BackColor = Color.FromArgb(56, 56, 56)
    '            Panel23.BackColor = Color.FromArgb(56, 56, 56)
    '            Panel24.BackColor = Color.FromArgb(56, 56, 56)
    '        End If
    '#End Region

    '    End Sub

#End Region

#Region "Outros"
#Region "PlaceHolders"
    'Private Sub tbAdmAddEmail_TextChanged(sender As Object, e As EventArgs) Handles tbAdmAddEmail.TextChanged
    '    With tbAdmAddEmail
    '        If .Text = "" Then
    '            .Text = "E-mail"
    '            .ForeColor = Color.FromArgb(200, 200, 200)
    '        End If
    '        If .TextLength > 5 Then
    '            If StrReverse(StrReverse(.Text).Remove(5)) = "E-mail" Then
    '                .Text = .Text.Remove(.TextLength - 5)
    '                .ForeColor = Color.DimGray
    '                .SelectionStart = .TextLength
    '                .ScrollToCaret()
    '            End If
    '        End If
    '    End With
    'End Sub

    'Private Sub tbAdmAddEmail_KeyDown(sender As Object, e As KeyEventArgs) Handles tbAdmAddEmail.KeyDown
    '    With tbAdmAddEmail
    '        If .Text = "E-mail" And .ForeColor = Color.FromArgb(200, 200, 200) Then
    '            If e.KeyCode = Keys.Right Or e.KeyCode = Keys.Left Or e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.End Then
    '                e.Handled = True
    '            End If
    '        End If
    '    End With
    'End Sub

    'Private Sub tbAdmAddEmail_MouseDown(sender As Object, e As MouseEventArgs) Handles tbAdmAddEmail.MouseDown
    '    controlePlace5 = True
    '    With tbAdmAddEmail
    '        If .Text = "E-mail" And .ForeColor = Color.FromArgb(200, 200, 200) Then
    '            .SelectionStart = .TextLength
    '            .SelectionLength = 0
    '            .SelectionStart = 0
    '            .ScrollToCaret()
    '        End If
    '    End With
    'End Sub

    'Private Sub tbAdmAddEmail_MouseMove(sender As Object, e As MouseEventArgs) Handles tbAdmAddEmail.MouseMove
    '    If controlePlace5 Then
    '        With tbAdmAddEmail
    '            If .Text = "E-mail" And .ForeColor = Color.FromArgb(200, 200, 200) Then
    '                tbAdmAddEmail.Select(0, 0)
    '            End If
    '        End With
    '    End If
    'End Sub



    'Private Sub tbAdmAddEmail_MouseUp(sender As Object, e As MouseEventArgs) Handles tbAdmAddEmail.MouseUp
    '    controlePlace5 = False
    'End Sub



#End Region

    'Impede Entrada de Não Número em Saldos
    Private Sub tbCartaoAddSaldo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbCartaoAddSaldo.KeyPress
        If Not Char.IsNumber(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    'Botões Limpar e Cancelar
    Public Sub btAdmAddLimpar_Click(sender As Object, e As EventArgs) Handles btAdmAddLimpar.Click
        tbAdmAddEmail.Clear()
        tbAdmAddLogin.Clear()
        tbAdmAddNome.Clear()
        tbAdmAddSenha.Clear()
    End Sub
    Public Sub btAdmDelCancelar_Click(sender As Object, e As EventArgs) Handles btAdmDelCancelar.Click
        tbAdmDelBusca.Clear()
        tbAdmDelEmail.Clear()
        tbAdmDelLogin.Clear()
        tbAdmDelNome.Clear()
    End Sub

    Private Sub btCartaoAddLimpar_Click(sender As Object, e As EventArgs) Handles btCartaoAddLimpar.Click
        tbCartaoAddEmail.Clear()
        tbCartaoAddMat.Clear()
        tbCartaoAddNome.Clear()
        tbCartaoAddSaldo.Clear()
    End Sub





#End Region

End Class