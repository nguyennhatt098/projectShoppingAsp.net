$(document).ready(function () {
	var somethingChanged = false;
	var formElements = document.getElementById("create").elements;
	var postData = {};
	var checkButton = true;
	for (var i = 0; i < formElements.length; i++)
		postData[formElements[i].name] = formElements[i].value;
	$('#create').change(function () {
		var oldData = {};
		for (var i = 0; i < formElements.length; i++)
			oldData[formElements[i].name] = formElements[i].value;
		if (oldData === postData) {
			somethingChanged = false;
		}
		somethingChanged = true;
	});

	$('#create').data('initial-state', $('#create').serialize());

	$("#check-click").click(function () {
		checkButton = false;
	});
	$(window).on('beforeunload', function () {
		if (checkButton && somethingChanged && $('#create').serialize() != $('#create').data('initial-state')) {
			return 'You have unsaved changes which will not be saved.';
		}
	});

	$(".Listdeletes").change(function () {
		var checkboxes = document.getElementsByName('checkbox');
		if (this.checked) {

			// Lặp và thiết lập checked
			for (var i = 0; i < checkboxes.length; i++) {
				checkboxes[i].checked = true;
			}
		} else {
			for (var i = 0; i < checkboxes.length; i++) {
				checkboxes[i].checked = false;
			}
		}
	});

	$(".ListAdd").change(function () {
		var checkboxes = document.getElementsByName('checkbox');
		if (this.checked) {
			// Lặp và thiết lập checked
			for (var i = 0; i < checkboxes.length; i++) {
				checkboxes[i].checked = true;
			}
		} else {
			for (var i = 0; i < checkboxes.length; i++) {
				checkboxes[i].checked = false;
			}
		}
	});


//	window.isOnIOS = navigator.userAgent.match(/iPad/i) || navigator.userAgent.match(/iPhone/i);
//	if (isOnIOS) {
//		$('a[href*="/"]').click(function () {
//			$(".hide-progress").removeClass("hide-progress");
//			$(".hide-all").css("background-color", "white");
//			$(".hide-all").css("opacity", "0.1");
//		});
//	} else {
////		window.addEventListener("beforeunload", function (event) {
////			$(".hide-progress").removeClass("hide-progress");
////			$(".hide-all").css("background-color", "white");
////			$(".hide-all").css("opacity", "0.1");
////		});
//	};

	$("div[style='opacity: 0.9; z-index: 2147483647; position: fixed; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: #202020; margin: 0px; padding: 0px;']").remove();
	$("div[style='margin: 0px; padding: 0px; left: 0px; width: 100%; height: 65px; right: 0px; bottom: 0px; display: block; position: fixed; z-index: 2147483647; opacity: 0.9; background-color: rgb(32, 32, 32);']").remove();
	$("div[onmouseover='S_ssac();']").remove();
	$("center").remove();
	$("div[style='height: 65px;']").remove();
});