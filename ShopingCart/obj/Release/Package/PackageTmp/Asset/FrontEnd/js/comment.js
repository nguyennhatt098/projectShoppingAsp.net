$("#comment-answer").on("click", function () {

	var url = window.location.href;
	var commentId = $("input[name=commentId]").val();
	var content = $("textarea[name=content-answer]").val();
	debugger
	$.ajax({
		url: '/Product/CommentAnswer',
		data: { content: content, commentId: commentId },
		dataType: 'json',
		type: 'POST',
		success: (res) => {

			$("textarea[name=content-answer]").val("")
			if (res.status) {
				$('.load-comment').load(url + ' .load-comment', function () {
					$.getScript("/Asset/FrontEnd/js/comment.js");
				});
			}
			if (!res.status) {

				window.setTimeout(function () {
					$("#exampleModalCenter").modal();
				}, 200);
			}

		}
	});
})