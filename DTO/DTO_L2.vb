Public Class DTO_L2

    Public ID_Ref As String
    Public Cat_ref As String
    Public Rank_Ref As String
    Public Type_ref As String
    Public Grade_Ref As String
    Public User_Name_Ref As String
    Public URL_Ref As String
    Public Uploads_Ref As String
    Public Subscribers_Ref As String
    Public Vido_Views_Ref As String

    Public Uploads As String
    Public Subscribers As String
    Public Video_Views As String
    Public Country As String
    Public Channel_Type As String
    Public User_Created As String
    Public Social_Balde_Rank As String
    Public Subscriber_Rank As String
    Public Video_Views_Rank As String
    Public Country_Rank As String
    Public Channel_Type_Rank As String
    Public Subscribers_For_The_Last_Thirty_Days As String
    Public Estimated_Monthly_Earnings As String
    Public Video_Views_For_The_Last_Thirty_Days As String
    Public Estimated_Yearly_Earnings As String

    Public stats As String

    Public url_Ref_similarChannels As String

    Public Similar_Top_Subscribers As String
    Public Similar_Top_Video_Views As String
    Public Similar_Social_Blade_Rank As String

    Public Sub New()
        Initialize()
    End Sub

    Private Sub Initialize()
        ID_Ref = "Unspecified"
        Cat_ref = "Unspecified"
        Rank_Ref = "Unspecified"
        Type_ref = "Unspecified"
        Grade_Ref = "Unspecified"
        User_Name_Ref = "Unspecified"
        URL_Ref = "Unspecified"
        Uploads_Ref = "Unspecified"
        Subscribers_Ref = "Unspecified"
        Vido_Views_Ref = "Unspecified"

        Uploads = "Unspecified"
        Subscribers = "Unspecified"
        Video_Views = "Unspecified"
        Country = "Unspecified"
        Channel_Type = "Unspecified"
        User_Created = "Unspecified"
        Social_Balde_Rank = "Unspecified"
        Subscriber_Rank = "Unspecified"
        Video_Views_Rank = "Unspecified"
        Country_Rank = "Unspecified"
        Channel_Type_Rank = "Unspecified"
        Subscribers_For_The_Last_Thirty_Days = "Unspecified"
        Estimated_Monthly_Earnings = "Unspecified"
        Video_Views_For_The_Last_Thirty_Days = "Unspecified"
        Estimated_Yearly_Earnings = "Unspecified"

        stats = "Unspecified"

        url_Ref_similarChannels = "Unspecified"
    End Sub

    Public Overrides Function ToString() As String
        Return ID_Ref & vbTab & Cat_ref & vbTab & Rank_Ref & vbTab & Type_ref & vbTab & Grade_Ref & vbTab & User_Name_Ref & vbTab & URL_Ref & vbTab & Uploads_Ref & vbTab & Subscribers_Ref & vbTab & Vido_Views_Ref & vbTab & Uploads & vbTab & Subscribers & vbTab & Video_Views & vbTab & Country & vbTab & Channel_Type & vbTab & User_Created & vbTab & Social_Balde_Rank _
            & vbTab & Subscriber_Rank & vbTab & Video_Views_Rank & vbTab & Country_Rank & vbTab & Channel_Type_Rank & vbTab & Subscribers_For_The_Last_Thirty_Days & vbTab & Estimated_Monthly_Earnings & vbTab & Video_Views_For_The_Last_Thirty_Days & vbTab & Estimated_Yearly_Earnings & vbTab & stats & vbTab & url_Ref_similarChannels & vbNewLine
    End Function

    Public Function fails() As String

        Return ID_Ref & vbTab & Cat_ref & vbTab & Type_ref & vbTab & Rank_Ref & vbTab & Grade_Ref & vbTab & User_Name_Ref & vbTab & URL_Ref & vbTab & Uploads_Ref & vbTab & Subscribers_Ref & vbTab & Vido_Views_Ref & vbNewLine

    End Function

End Class
