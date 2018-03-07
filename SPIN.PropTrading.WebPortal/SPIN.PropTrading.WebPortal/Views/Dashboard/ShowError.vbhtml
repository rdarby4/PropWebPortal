@Code
    ViewData("Title") = "ShowError"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2 class="heading">An Error Has Occured</h2>

<div class="heading">
<p>Details: @ViewData("Msg")</p>

<br/>

<p>If the problem persists, please contact support.</p>

</div>

