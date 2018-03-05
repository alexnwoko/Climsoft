﻿Public Class frmNewSynopticDataForManyElements
    Private bFirstLoad As Boolean = True

    Private Sub frmNewSynopticDataForManyElements_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If bFirstLoad Then
            InitaliseDialog()
            bFirstLoad = False
        End If
    End Sub

    Private Sub InitaliseDialog()
        Dim dctNavigationFields As New Dictionary(Of String, List(Of String))
        Dim dctNavigationKeyControls As New Dictionary(Of String, ucrBaseDataLink)

        dctNavigationFields.Add("stationId", New List(Of String)({"stationId"}))
        dctNavigationFields.Add("yyyy", New List(Of String)({"yyyy"}))
        dctNavigationFields.Add("mm", New List(Of String)({"mm"}))
        dctNavigationFields.Add("dd", New List(Of String)({"dd"}))
        dctNavigationFields.Add("hh", New List(Of String)({"hh"}))

        ucrNavigation.SetFields(dctNavigationFields)
        ucrNavigation.SetTableName("form_synoptic_2_ra1")

        dctNavigationKeyControls.Add("stationId", ucrStationSelector)
        dctNavigationKeyControls.Add("yyyy", ucrYearSelector)
        dctNavigationKeyControls.Add("mm", ucrMonth)
        dctNavigationKeyControls.Add("dd", ucrDay)
        dctNavigationKeyControls.Add("hh", ucrHour)

        ucrNavigation.SetKeyControls(dctNavigationKeyControls)

        ucrNavigation.PopulateControl()
        ucrSynopticDataForManyElements.setYearMonthDayHourLink(ucrYearControl:=ucrYearSelector, ucrMonthControl:=ucrMonth, ucrDayControl:=ucrDay, ucrHourControl:=ucrHour)
        AssignLinkToKeyField(ucrSynopticDataForManyElements)
        ucrSynopticDataForManyElements.PopulateControl()
    End Sub

    Private Sub AssignLinkToKeyField(ucrControl As ucrBaseDataLink)
        ucrControl.AddLinkedControlFilters(ucrStationSelector, "stationId", "==", strLinkedFieldName:="stationId", bForceValuesAsString:=True)
        ucrControl.AddLinkedControlFilters(ucrYearSelector, "yyyy", "==", strLinkedFieldName:="Year", bForceValuesAsString:=False)
        ucrControl.AddLinkedControlFilters(ucrMonth, "mm", "==", strLinkedFieldName:="MonthId", bForceValuesAsString:=False)
        ucrControl.AddLinkedControlFilters(ucrDay, "dd", "==", strLinkedFieldName:="day", bForceValuesAsString:=False)
        ucrControl.AddLinkedControlFilters(ucrHour, "hh", "==", strLinkedFieldName:="24Hrs", bForceValuesAsString:=False)
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ucrSynopticDataForManyElements.Clear()
    End Sub

    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        If ucrSynopticDataForManyElements.bUpdating Then
            'Possibly we should be cloning and then updating here
        Else
            clsDataConnection.db.form_synoptic_2_ra1.Add(ucrSynopticDataForManyElements.fs2Record)
        End If
        clsDataConnection.SaveUpdate()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class