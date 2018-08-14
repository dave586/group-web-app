
$(document).ready(function() {
    $("#submitTest").click(function () {
            
        if(model.testAnswers()) {
            $("#data").val(JSON.stringify(model.viewModel));
            $("form").submit();
        }
    });

    var data = @Html.Raw(Json.Encode(Model));

    var model =
    {
        viewModel: data,
        testAnswers:function () {
            var allAnswered = true;
            for(var q=0; q < this.viewModel.Questions.length; q++) {
                if(ul.utils.isNullOrEmpty(this.viewModel.Questions[q].Answer)) {
                    allAnswered = false;
                    break;
                }
            }
                
            if(!allAnswered) {
                if(confirm('Some questions have not been answered. Are you sure you want to continue?')) {
                    return true;
                } else {
                    return false;
                }
            }
            return true;
        }
    };

    ko.applyBindings(model);

});