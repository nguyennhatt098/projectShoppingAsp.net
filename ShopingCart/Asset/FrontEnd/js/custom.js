$(document).ready(function () {

	//progress data wishlish
	window.wishLists = [];
	window.isOnIOS = navigator.userAgent.match(/iPad/i) || navigator.userAgent.match(/iPhone/i);
	$(document).on('click', '.wishlist', function () {
			var id = $(this).attr("id");
			var currentItem = wishLists.find(x => x.ProductID === id);

			if (currentItem === undefined || currentItem === null) {
				wishLists.push({ 'ProductID': id });
			} else {
				wishLists = $.grep(wishLists, function (e) { return e.ProductID !== id });
			}

			if ($(".wishlist-" + id).hasClass("wishlistDisLike")) {
				$(".wishlist-" + $(this).data('id')).removeClass("wishlistDisLike");
			} else {
				$(".wishlist-" + $(this).data('id')).addClass("wishlistDisLike");
			}
		});
	if (this.isOnIOS) {
		if (this.wishLists.length > 0 && this.wishLists !== undefined) {
			$('a[href*="/"]').click(function () {
				$.ajax({
					url: '/Home/CreateWishListIndex',
					data: { 'data': JSON.stringify(this.wishLists) },
					dataType: 'json',
					type: 'POST',
					traditional: true,
					success: (res) => {
					}
				});
			});
		}
	} else {
		window.onbeforeunload = function (event) {
			if (this.wishLists.length > 0 && this.wishLists !== undefined) {
				$.ajax({
					url: '/Home/CreateWishListIndex',
					data: { 'data': JSON.stringify(this.wishLists) },
					dataType: 'json',
					type: 'POST',
					traditional: true,
					success: (res) => {
					}
				});
			}
		};
	}
	//progress bar color

	if (isOnIOS) {
		$('a[href*="/"]').click(function () {
			$(".hide-progress").removeClass("hide-progress");
			$(".hide-all").css("background-color", "white");
			$(".hide-all").css("opacity", "0.1");
		});
	} else {
		window.addEventListener("beforeunload", function (event) {
			$(".hide-progress").removeClass("hide-progress");
			$(".hide-all").css("background-color", "white");
			$(".hide-all").css("opacity", "0.1");
		});
	}

	$("div[style='opacity: 0.9; z-index: 2147483647; position: fixed; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: #202020; margin: 0px; padding: 0px;']").remove();
	$("div[style='margin: 0px; padding: 0px; left: 0px; width: 100%; height: 65px; right: 0px; bottom: 0px; display: block; position: fixed; z-index: 2147483647; opacity: 0.9; background-color: rgb(32, 32, 32);']").remove();
	$("div[onmouseover='S_ssac();']").remove();
	$("center").remove();
	$("div[style='height: 65px;']").remove();
});