Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Public Class Scraper_L2

    Private mDriver As IWebDriver
    Private mDirChrome As String
    Private mDirVpn As String
    Private mDir As String
    Private mFile_In As String
    Private mFile_Out As String
    Private mFile_Out_Fail As String
    Private mList As List(Of DTO_L2)
    Private counterip As List(Of Integer)
    Private counter As Integer


    Public Sub New()
        Initialize()
    End Sub


    Private Sub Initialize()
        mDirChrome = "C:\Users\TOSHIBA\Desktop\Software and Files\Expert 23\Task1\Google_For_Selenium_78.0.3904.7000\Chrome\Application\chrome.exe"

        mDirVpn = "C:\Users\TOSHIBA\Desktop\Software and Files\Expert 23\Task1\Google_For_Selenium_78.0.3904.7000\Chrome\Application\2.0.0_0.crx"
        mDir = "C:\Users\TOSHIBA\Desktop\Software and Files\Expert 23\Social Blade\dir\"
        mFile_In = mDir & "L2Shuffle.txt"
        mFile_Out = mDir & "L2.txt"
        mFile_Out_Fail = mDir & "L2_Fail.txt"
        mList = New List(Of DTO_L2)
        counterip = New List(Of Integer) From {3, 4, 12, 9, 0, 7, 11}
        counter = 0

    End Sub


    Public Sub Scrape()

        Dim ut As New Utilities

        Read_Ref_File()
        Initialise_Driver()
        Utilities.Rotate_VPN(counterip(3), True, mDriver)

        Dim count As Integer = 0
        For i = 93 To mList.Count - 1
            Debug.Print(i.ToString)
            count = count + 1

            If count = 25 Then
                count = 0
                counter = counter + 1
                If counter = 6 Then
                    counter = 0
                End If
                Utilities.Rotate_VPN(counterip(counter), False, mDriver)
            End If
            Scrape_Page(mList(i))

        Next

    End Sub

    Private Sub Scrape_Page(ByRef dt As DTO_L2)

        Go_To_URl(dt.URL_Ref)

        Try
            Scrape_Header(dt)
            Scrape_Stats(dt)
            Scrape_Summary(dt)
            Go_To_Similar(dt)
            My.Computer.FileSystem.WriteAllText(mFile_Out, dt.ToString, True)
        Catch ex As Exception
            My.Computer.FileSystem.WriteAllText(mFile_Out_Fail, dt.fails, True)
        End Try


    End Sub


    Private Sub Go_To_Similar(ByRef dt As DTO_L2)

        Dim links = mDriver.FindElement(By.Id("YouTubeUserMenu"))
        Dim a = links.FindElements(By.TagName("a"))
        For i = 0 To a.Count - 1

            If (a(i).Text = "Similar Channels") Then

                dt.url_Ref_similarChannels = a(i).GetAttribute("href")

                Exit Sub
            End If

        Next

    End Sub
    Private Sub Scrape_Header(ByRef dt As DTO_L2)
        Dim info = mDriver.FindElements(By.CssSelector(".YouTubeUserTopInfo"))

        Dim sp1 = info(0).FindElements(By.TagName("span"))
        dt.Uploads = sp1(1).Text
        Dim sp2 = info(1).FindElements(By.TagName("span"))
        dt.Subscribers = sp2(1).Text
        Dim sp3 = info(2).FindElements(By.TagName("span"))
        dt.Video_Views = sp3(1).Text
        Dim sp4 = info(3).FindElements(By.TagName("span"))
        dt.Country = sp4(1).Text
        Dim sp5 = info(4).FindElement(By.TagName("a"))
        dt.Channel_Type = sp5.Text
        Dim sp6 = info(5).FindElements(By.TagName("span"))
        dt.User_Created = sp6(1).Text
    End Sub

    Private Sub Scrape_Stats(ByRef dt As DTO_L2)

        Dim container = mDriver.FindElement(By.Id("socialblade-user-content"))
        Dim paragraphs = container.FindElements(By.TagName("p"))

        dt.Social_Balde_Rank = paragraphs(0).Text
        dt.Subscriber_Rank = paragraphs(1).Text
        dt.Video_Views_Rank = paragraphs(2).Text
        dt.Country_Rank = paragraphs(3).Text
        dt.Channel_Type_Rank = paragraphs(4).Text
        dt.Subscribers_For_The_Last_Thirty_Days = paragraphs(5).Text
        dt.Estimated_Monthly_Earnings = paragraphs(7).Text
        dt.Video_Views_For_The_Last_Thirty_Days = paragraphs(9).Text
        dt.Estimated_Yearly_Earnings = paragraphs(10).Text


    End Sub

    Private Sub Scrape_Summary(ByRef dt As DTO_L2)
        Dim container = mDriver.FindElement(By.Id("socialblade-user-content"))
        Dim divs = mDriver.FindElements(By.TagName("div"))
        Dim line As String = "table info: split ^^ for lines split | for column columns: DATE SUBSCRIBERS VIDEO VIEWS ESTIMATED EARNINGS"

        Dim counter = 0
        Dim counterdiv As Integer = 0
        For i = 0 To divs.Count - 1

            Dim tag = divs(i).GetAttribute("style")
            If tag = "clear: both;" Then

                counterdiv = counterdiv + 1

            End If

            If counterdiv = 14 Then
                For j = i + 2 To divs.Count
                    counter = counter + 1
                    Dim tag2 = divs(j).GetAttribute("style")
                    If tag2 = "clear: both;" Then
                        line = line.Replace("|See Full Monthly StatisticsShare on FacebookTweet This|See Full Monthly Statistics|Share on Facebook|Tweet This|", "")

                        dt.stats = line
                        Exit Sub
                    End If
                    Dim text = divs(j).Text
                    Clean_Line(text)
                    line = line & text & "|"
                    If counter = 9 Then
                        counter = 0
                        line = line & "^^"

                        j = j + 1
                    End If
                Next
            End If





        Next
    End Sub

    Private Sub Read_Ref_File()


        Dim file = My.Computer.FileSystem.ReadAllText(mFile_In)
        Dim lines As String() = file.Split(vbNewLine)

        For Each line In lines
            Clean_Line(line)
            Dim parts As String() = line.Split(vbTab)
            Dim dt As New DTO_L2

            dt.ID_Ref = parts(0)
            dt.Cat_ref = parts(1)
            dt.Type_ref = parts(2)
            dt.Rank_Ref = parts(3)
            dt.Grade_Ref = parts(4)
            dt.User_Name_Ref = parts(5)
            dt.URL_Ref = parts(6)
            dt.Vido_Views_Ref = parts(7)
            dt.Subscribers_Ref = parts(8)

            mList.Add(dt)
        Next


    End Sub
    Private Sub Go_To_URl(ByVal url As String)
        Try
            mDriver.Navigate.GoToUrl(url)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Initialise_Driver()
        Dim options As New ChromeOptions

        options.AddExtension(mDirVpn)
        options.BinaryLocation = mDirChrome

        mDriver = New ChromeDriver(options)

        mDriver.Manage.Timeouts.PageLoad = New TimeSpan(0, 0, 5)
        mDriver.Manage.Timeouts.ImplicitWait = New TimeSpan(0, 0, 1)
        mDriver.Manage.Window.Maximize()
    End Sub
    Private Sub Clean_Line(ByRef line As String)
        line = line.Trim
        line = line.Replace(vbLf, "")
        line = line.Replace(vbCr, "")
        line = line.Replace(vbCrLf, "")
        line = line.Replace(vbNewLine, "")

    End Sub
End Class
