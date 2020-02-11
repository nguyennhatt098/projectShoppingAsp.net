
$(document).ready(function () {
	$('#addToCart').click(function () {
		var quantity = document.getElementById("quantity").value;
		var productId = $(this).attr("valueId");
		window.location.href = '/them-vao-gio-hang?productID=' + productId + '&quantity=' + quantity
	});
});

//progress event click comment
$(document).on('click', '.comment-id', function () {
	var commentId = $(this).attr("checkComment");
	if ($(".active-question-" + commentId)[0]) {
		$('.show-comment-id-' + commentId).removeClass("active-question-" + commentId);
		$('.show-comment-id-' + commentId).hide("slow");
	} else {
		$('.show-comment-id-' + commentId).addClass("active-question-" + commentId);
		$('.show-comment-id-' + commentId).show("slow");
	}
});

//progress event click review question
$(document).on('click', '.review-id', function () {
	var reviewId = $(this).attr("checkReview");
	if ($(".active-review-" + reviewId)[0]) {
		$('.show-review-' + reviewId).removeClass("active-review-" + reviewId);
		$('.show-review-' + reviewId).hide("slow");
	} else {
		$('.show-review-' + reviewId).addClass("active-review-" + reviewId);
		$('.show-review-' + reviewId).show("slow");
	}
});

$(document).on('click', '.review-answer', function () {
	var reviewId = $(this).attr("submitReview");
	var contentAnswerReview = $(".content-review-answer-" + reviewId).val();
	if (contentAnswerReview === null || contentAnswerReview.match(/^ *$/) !== null) return;
	var postData = { ReviewId: reviewId, Content: contentAnswerReview };
	$.ajax({
		url: '/Product/CommentReview',
		data: { 'data': JSON.stringify(postData) },
		dataType: 'json',
		type: 'POST',
		success: (res) => {
			if (res.status) {
				$.ajax({
					url: urlReview,
					type: 'GET',
					success: (res) => {
						if (res) {
							document.getElementById("show-all-reviews").innerHTML = getReviews(res.items);
							document.getElementById("show-pagination-reviews").innerHTML = getPaginationComment(res.TotalRecords);
							$('.show-review-' + reviewId).addClass("active-review-" + reviewId);
							$('.show-review-' + reviewId).show();
						}
					}
				});
			}
			if (!res.status) {
				window.setTimeout(function () {
					$("#exampleModalCenter").modal();
				},
					200);
			}
		}
	});
});
//window.addEventListener("load",
//	function () {
//		var url = window.location.href.split('?');
//		if (url.length > 1) {
//			if (url[1].split('=')[0] == 'pageRv') {
//				if ($(".check-active").hasClass("active")) {
//					$(".check-active").removeClass("active");
//					$(".show-active").addClass("active");
//				}
//			}
//		}
//	});
//post answer comment
$(document).on('click', '.comment-answer',
	function () {
		var commentId = $(this).attr("commentId");
		var contentAnswer = $(".content-answer-" + commentId).val();
		var postData = { content: contentAnswer, commentId: commentId };
		$.ajax({
			url: '/Product/CommentAnswer',
			data: postData,
			dataType: 'json',
			type: 'POST',
			success: (res) => {
				if (res.status) {
					$.ajax({
						url: url,
						type: 'GET',
						success: (res) => {
							if (res) {
								document.querySelector(".show-all-comments").innerHTML = getComments(res.items);
								document.getElementById("show-pagination-comment").innerHTML = getPaginationComment(res.TotalRecords);
								$('.show-comment-id-' + commentId).addClass("active-question-" + commentId);
								$('.show-comment-id-' + commentId).show();
							}
						}
					});
				}
				if (!res.status) {
					window.setTimeout(function () {
						$("#exampleModalCenter").modal();
					}, 200);
				}
			}
		});
	});
$("#comment").on("click",
	function () {
		var productId = $("input[name=productId]").val();
		var content = $("textarea[name=content]").val();
		$.ajax({
			url: '/Product/Comment',
			data: { content: content, productId: productId },
			dataType: 'json',
			type: 'POST',
			success: (res) => {
				if (res.status) {
					$("textarea[name=content]").val("");
					callBackComments();
				}
				if (!res.status) {
					window.setTimeout(function () {
						$("#exampleModalCenter").modal();
					}, 200);
				}
			}
		});
	});

function callBackComments() {
	$.ajax({
		url: url,
		type: 'GET',
		success: (res) => {
			if (res) {
				document.querySelector(".show-all-comments").innerHTML = this.getComments(res.items);
				document.getElementById("show-pagination-comment").innerHTML = this.getPaginationComment(res.TotalRecords);
			}
		}
	});
}

$.ajax({
	url: '/Home/GetProductHot',
	type: 'GET',
	success: (res) => {
		if (res) {
			document.querySelector("#productHot").innerHTML = this.getData(res);
			$("#productHot").owlCarousel({
				loop: false,
				nav: true,
				navText: ['<i class="fa fa-long-arrow-left"></i>', '<i class="fa fa-long-arrow-right"></i>'],
				responsive: {
					0: {
						items: 1
					},
					767: {
						items: 2
					},
					1000: {
						items: 3
					},
					1200: {
						items: 4
					}
				}
			});
		}
	}
});

function getData(data) {
	var userId = this.getCookie('UserID');
	var txt = '';
	for (i in data) {
		var checkWishList = userId == undefined
			? false
			: getWishListByUserProduct(data[i].wishLists, userId, data[i].Id);
		var url = /chi-tiet-san-pham/ + data[i].Slug + '-' + data[i].Id;
		var urlCart = "/them-vao-gio-hang?productID=" + data[i].Id + "&quantity=1";
		txt += "<div class='col-md-12'>";
		txt += "<div class='product-wrapper mb-40'>";
		txt += "<div class='product-img'>";
		txt += "<a href=" + url + "><img  src=" + data[i].Images + " alt=" + data[i].Name + " /></a>";
		txt += "<span class='new-label-hot'>Hot</span>";
		txt += "<div class='product-action'>";
		txt += "<a href=" + urlCart + "><i class='pe-7s-cart' ></i></a>";
		if (userId == undefined) {
			txt += "<a data-toggle='modal' data-target='#exampleModalCenter' ><i class='fa fa-heart'></i></a>";
		} else if (checkWishList) {
			txt += "<a class='wishlist wishlistDisLike wishlist-" +
				data[i].Id +
				"' id=" +
				data[i].Id +
				" data-id=" +
				data[i].Id +
				" href='javascript:void(0)'><i class='fa fa-heart'></i></a>";
		} else {
			txt += "<a class='wishlist wishlist-" +
				data[i].Id +
				"' id=" +
				data[i].Id +
				" data-id=" +
				data[i].Id +
				" href='javascript:void(0)'><i class='fa fa-heart'></i></a>";
		}
		txt += "<a href=" + url + "><i class='pe-7s-look'></i></a>";
		txt += "</div>";
		txt += "</div>";
		txt += "<div class='product-content'>";
		txt += "<div class='pro-title'>";
		txt += "<h3><a href='" + url + "'>" + data[i].Name + "</a></h3>";
		txt += "</div>";
		txt += "<div class='price-reviews'>";
		txt += "<div class='price-box'>";
		var nf = new Intl.NumberFormat();
		if (data[i].Sale_Price != null) {
			txt += "<span class='old-price product-price'>" +
				nf.format(data[i].Price) +
				"VND</span> <span class='price product-price'>" +
				nf.format(data[i].Sale_Price) +
				"VND</span>";
		} else {
			txt += "<span class='price product-price'>" + nf.format(data[i].Price) + "VND</span>";
		}
		txt += "</div>";
		txt += "<div class='pro-rating'>";
		if (data[i].AverageStar > 0) {
			var check = true;
			for (j = 1; j <= 5; j++) {
				if (j <= data[i].AverageStar) {
					txt += "<i class='fa fa-star'></i>";
				} else if (data[i].AverageStar % 1 !== 0 && check) {
					check = false;
					txt += "<i class='fa fa-star-half-o' aria-hidden='true'></i>";
				} else {
					txt += "<i class='fa fa-star-o' aria-hidden='true'></i>";
				}
			}
		}
		txt += "</div>";
		txt += "</div>";
		txt += "</div>";
		txt += "</div>";
		txt += "</div>";
	}
	return txt;
}

function getCookie(name) {
	var value = document.cookie;
	var parts = value.split("; " + name + "=");
	if (parts.length == 2) return parts[1];
}

function getWishListByUserProduct(wishLists, userId, productId) {
	for (var i = 0; i < wishLists.length; i++) {
		var obj = wishLists[i];
		var arr = obj.UserID == userId && obj.ProductID == productId;
		if (arr) return true;
	}
	return false;
}
var getUrlParameter = function getUrlParameter(sParam) {
	var sPageURL = window.location.search.substring(1),
		sURLVariables = sPageURL.split('&'),
		sParameterName,
		i;

	for (i = 0; i < sURLVariables.length; i++) {
		sParameterName = sURLVariables[i].split('=');

		if (sParameterName[0] === sParam) {
			return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
		}
	}
};
var currentUrl = window.location.pathname;
var productId = currentUrl.split('-')[currentUrl.split('-').length - 1];
var url = getUrlParameter('page')
	? '/Product/GetComments?id=' + productId + '&page=' + getUrlParameter('page')
	: '/Product/GetComments?id=' + productId;
$.ajax({
	url: url,
	type: 'GET',
	success: (res) => {
		if (res) {
			document.querySelector(".show-all-comments").innerHTML = this.getComments(res.items);
			document.getElementById("show-pagination-comment").innerHTML = this.getPaginationComment(res.TotalRecords);
		}
	}
});

function get_time_diff(date) {
	var datetime = new Date(parseInt(date.replace("/Date(", "").replace(")/", ""),10));

	if (isNaN(datetime)) return "";
	
	var datetime = datetime.getTime();
	var now = Date.now();
	var msec = (now - datetime) / 1000;

	var year = Math.floor(msec / 31104000);
	if (year > 0) return year + " năm trước";

	var month = Math.floor(msec / 2592000);
	if (month > 0) return month + " tháng trước";

	var week = Math.floor(msec / 604800);
	if (week > 0) return week + " tuần trước";

	var days = Math.floor(msec / 86400);
	if (days > 0) return days + " ngày trước";

	var hh = Math.floor(msec / 3600);
	if (hh > 0) return hh + " giờ trước";
	
	var mm = Math.ceil(msec / 60);
	if (mm > 0) return mm + " phút trước";
}

function getComments(data) {
	var txt = '';
	for (i in data) {
		var total = data[i].Answers.length > 0 ? data[i].Answers.length : "";
		txt += "<li class='media'>";
		txt += "<a href='javascript:void(0)' class='pull-left'>";
		if (data[i].User.Image == null) {
			txt += "<img src='/Contents/Uploads/files/user_1.jpg' alt='' class='img-circle'>";
		} else {
			txt += "<img src='" + data[i].User.Image + "' alt='' class='img-circle'>";
		}
		txt += "</a>";
		txt += "<div class='media-body'>";
		txt += "<span class='text-muted pull-right'>";
		txt += "<small class='text-muted'>" + this.get_time_diff(data[i].CreatedDate) + "</small>";
		txt += "</span>";
		txt += "<strong class='text-success'>" + data[i].User.UserName + "</strong>";
		txt += "<p>" + data[i].Question + "</p>";
		txt += "<a href='javascript:void(0)' class='comment-id' checkComment='" + data[i].Id + "'><span class='glyphicon glyphicon-share-alt'></span>" + total + " Trả lời</a>";
		txt += "<div style='display: none;margin-bottom: 40px' class='show-comment-id-" + data[i].Id + "'>";
		txt += "<div style='margin-top:30px;height:150px;'>";
		txt += "<form class='answer-form'>";
		txt += "<textarea class='form-control content-answer-" + data[i].Id + "' commentIds='" + data[i].Id + "' name='content-answer' placeholder='write a comment...' rows='3'></textarea><br>";
		txt += "<button commentId='" + data[i].Id + "' id='comment-answer' type='button' class='btn btn-info pull-right comment-answer'>Gửi</button>";
		txt += "</form>";
		txt += "</div>";
		for (var j = 0; j < data[i].Answers.length; j++) {
			var itemAnswer = data[i].Answers;
			txt += "<div style='margin-top:40px'>";
			txt += "<a href='javascript:void(0)' class='pull-left'>";
			if (itemAnswer[j].User.Image == null) {
				txt += "<img src='/Contents/Uploads/files/user_1.jpg' alt='' class='img-circle'>";
			} else {
				txt += "<img src='" + itemAnswer[j].User.Image + "' alt='' class='img-circle'>";
			}
			txt += "</a>";
			txt += "<span class='text-muted pull-right'>";
			txt += "<small class='text-muted'>" + this.get_time_diff(itemAnswer[j].CreatedDate) + "</small>";
			txt += "</span>";
			txt += "<strong style='margin-left:15px;' class='text-success'>" + itemAnswer[j].User.UserName + "</strong>";
			txt += "<p style='margin-left:80px;'>" + itemAnswer[j].Content + " </p>";
			txt += "</div>";
		}
		txt += "</div>";
		txt += "</div>";
		txt += "</div>";
	}
	return txt;
}

function getPaginationComment(TotalRecords) {
	var txt = '';
	var pageIndex = getUrlParameter('page') ? getUrlParameter('page') : 1;
	var totalPage = Math.ceil(TotalRecords / 6);

	var start = (pageIndex <= 3 || totalPage == 0) ? 1 : pageIndex - 3;
	var end = (totalPage <= pageIndex + 3) ? totalPage : pageIndex + 3;
	if (end >= totalPage) {
		end = totalPage;
	}
	txt += "<div class='content-sortpagibar'>";
	txt += "<ul class='shop-pagi display-inline'>";

	if (TotalRecords) {
		if (pageIndex > 1)
			txt += "<li class='navigate' events='back'><a href='javascript:void(0)' ><i class='fa fa-angle-left'></i></a></li>";
		for (var i = start; i <= end; i++) {
			if (pageIndex == i) {
				txt += "<li class='active navigate'><a href='javascript:void(0)'>" + i + "</a></li>";
			} else {
				txt += "<li class='navigate'><a href='javascript:void(0)' >" + i + "</a></li>";
			}
		}
		if (pageIndex < totalPage) {
			txt += "<li class='navigate' events='next'><a href='javascript:void(0)'><i class='fa fa-angle-right'></i></a></li>";
		}
	} else {
		txt += "<ul class='shop-pagi display-inline'>";
		txt += "<li class='active'><a href='javascript:void(0)'>1</a></li>";
		txt += "</ul>";
	}
	return txt;
}
$(document).on("click", ".navigate", function () {
	$('html, body').animate({
		scrollTop: $(".show-all-comments").offset().top - 350
	}, 1000);
	var urlNavigate = '';
	var pageIndex = getUrlParameter('page') ? getUrlParameter('page') : 1;
	if ($(this).text() == '') {
		if ($(this).attr("events") == 'back') {
			pageIndex -= 1;
			urlNavigate = '/Product/GetComments?id=' + productId + '&page=' + pageIndex;
		} else {
			pageIndex = +pageIndex + 1;
			urlNavigate = '/Product/GetComments?id=' + productId + '&page=' + pageIndex;
		}
	} else {
		pageIndex = $(this).text();
		urlNavigate = '/Product/GetComments?id=' + productId + '&page=' + $(this).text();
	}
	$.ajax({
		url: urlNavigate,
		type: 'GET',
		success: (res) => {
			if (res.items) {
				var pageIndexReview = getUrlParameter('pageRv') ? getUrlParameter('pageRv') : null;
				document.querySelector(".show-all-comments").innerHTML = getComments(res.items);
				if (pageIndexReview == null) {
					window.history.pushState('Chi tiết sản phẩm', 'Title', currentUrl + '?page=' + pageIndex);
				} else {
					window.history.pushState('Chi tiết sản phẩm', 'Title', currentUrl + '?page=' + pageIndex + '&pageRv=' + pageIndexReview);
				}
				document.getElementById("show-pagination-comment").innerHTML = getPaginationComment(res.TotalRecords);
			}
		}
	});
});



function getReviews(data) {
	var txt = '';
	for (i in data) {
		var total = data[i].AnswerReviews.length > 0 ? data[i].AnswerReviews.length : "";
		txt += "<li class='media'>";
		txt += "<a href='javascript:void(0)' class='pull-left'>";
		if (data[i].User.Image == null) {
			txt += "<img src='/Contents/Uploads/files/user_1.jpg' alt='' class='img-circle'>";
		} else {
			txt += "<img src='" + data[i].User.Image + "' alt='' class='img-circle'>";
		}
		txt += "</a>";
		txt += "<div class='media-body'>";
		txt += "<span class='text-muted pull-right'>";
		txt += "<small class='text-muted'>" + this.get_time_diff(data[i].CreatedDate) + "</small>";
		txt += "</span>";
		txt += "<strong class='text-success'>" + data[i].User.UserName + "</strong>";
		txt += "<p><span style='display: inline-block'>";
		for (var a = 1; a <= 5; a++) {
			if (a <= data[i].Rate) {
				txt += "<i style='color: #ffae00' class='fa fa-star'></i>";
			}
			else {
				txt += "<i style='color: #ffae00' class='fa fa-star-o' aria-hidden='true'></i>";
			}
		}
		txt += "<p>" + data[i].Content + "</p>";
		txt += "<a href='javascript:void(0)' class='review-id' checkReview='" + data[i].Id + "'><span class='glyphicon glyphicon-share-alt'></span>" + total + " Trả lời</a>";
		txt += "<div style='display: none;margin-bottom: 40px' class='show-review-" + data[i].Id + "'>";
		txt += "<div style='margin-top:30px;height:150px;'>";
		txt += "<form class='answer-form'>";
		txt += "<textarea class='form-control content-review-answer-" + data[i].Id + "' reviewId='" + data[i].Id + "' name='content-answer' placeholder='write a comment...' rows='3'></textarea><br>";
		txt += "<button submitReview='" + data[i].Id + "' type='button' class='btn btn-info pull-right review-answer'>Gửi</button>";
		txt += "</form>";
		txt += "</div>";
		for (var j = 0; j < data[i].AnswerReviews.length; j++) {
			var itemAnswer = data[i].AnswerReviews;
			txt += "<div style='margin-top:40px'>";
			txt += "<a href='javascript:void(0)' class='pull-left'>";
			if (itemAnswer[j].User.Image == null) {
				txt += "<img src='/Contents/Uploads/files/user_1.jpg' alt='' class='img-circle'>";
			} else {
				txt += "<img src='" + itemAnswer[j].User.Image + "' alt='' class='img-circle'>";
			}
			txt += "</a>";
			txt += "<span class='text-muted pull-right'>";
			txt += "<small class='text-muted'>" + this.get_time_diff(itemAnswer[j].CreatedDate) + "</small>";
			txt += "</span>";
			txt += "<strong style='margin-left:15px;' class='text-success'>" + itemAnswer[j].User.UserName + "</strong>";
			txt += "<p style='margin-left:80px;'>" + itemAnswer[j].Content + " </p>";
			txt += "</div>";
		}
		txt += "</div>";
		txt += "</div>";
		txt += "</div>";
	}
	return txt;
}

function getPaginationReviews(TotalRecords) {
	var txt = '';
	var pageIndex = getUrlParameter('pageRv') ? getUrlParameter('pageRv') : 1;
	var totalPage = Math.ceil(TotalRecords / 6);

	var start = (pageIndex <= 3 || totalPage == 0) ? 1 : pageIndex - 3;
	var end = (totalPage <= pageIndex + 3) ? totalPage : pageIndex + 3;
	if (end >= totalPage) {
		end = totalPage;
	}
	txt += "<div class='content-sortpagibar'>";
	txt += "<ul class='shop-pagi display-inline'>";

	if (TotalRecords) {
		if (pageIndex > 1)
			txt += "<li class='navigateRv' events='back'><a href='javascript:void(0)' ><i class='fa fa-angle-left'></i></a></li>";
		for (var i = start; i <= end; i++) {
			if (pageIndex == i) {
				txt += "<li class='active navigateRv'><a href='javascript:void(0)'>" + i + "</a></li>";
			} else {
				txt += "<li class='navigateRv'><a href='javascript:void(0)' >" + i + "</a></li>";
			}
		}
		if (pageIndex < totalPage) {
			txt += "<li class='navigateRv' events='next'><a href='javascript:void(0)'><i class='fa fa-angle-right'></i></a></li>";
		}
	} else {
		txt += "<div style='background:white' class='pro-desc'>";
		txt += "<h2>Chưa có đánh giá</h2>";
		txt += "</div>";
	}
	return txt;
}

var urlReview = getUrlParameter('pageRv')
	? '/Product/GetReviews?id=' + productId + '&pageRv=' + getUrlParameter('pageRv')
	: '/Product/GetReviews?id=' + productId;
$.ajax({
	url: urlReview,
	type: 'GET',
	success: (res) => {
		if (res) {
			document.getElementById("show-all-reviews").innerHTML = this.getReviews(res.items);
			document.getElementById("show-pagination-reviews").innerHTML = this.getPaginationReviews(res.TotalRecords);
		}
	}
});

$(document).on("click", ".navigateRv", function () {
	$('html, body').animate({
		scrollTop: $("#show-all-reviews").offset().top - 350
	}, 1000);
	var urlNavigate = '';
	var pageIndex = getUrlParameter('pageRv') ? getUrlParameter('pageRv') : 1;
	if ($(this).text() == '') {
		if ($(this).attr("events") == 'back') {
			pageIndex -= 1;
			urlNavigate = '/Product/GetReviews?id=' + productId + '&pageRv=' + pageIndex;
		} else {
			pageIndex = +pageIndex + 1;
			urlNavigate = '/Product/GetReviews?id=' + productId + '&pageRv=' + pageIndex;
		}
	} else {
		pageIndex = $(this).text();
		urlNavigate = '/Product/GetReviews?id=' + productId + '&pageRv=' + $(this).text();
	}
	$.ajax({
		url: urlNavigate,
		type: 'GET',
		success: (res) => {
			if (res.items) {
				var pageIndexComment = getUrlParameter('page') ? getUrlParameter('page') : null;
				document.getElementById("show-all-reviews").innerHTML = getReviews(res.items);
				if (pageIndexComment == null) {
					window.history.pushState('Chi tiết sản phẩm', 'Title', currentUrl + '?pageRv=' + pageIndex);
				} else {
					window.history.pushState('Chi tiết sản phẩm', 'Title', currentUrl + '?page=' + pageIndexComment + '&pageRv=' + pageIndex);
				}
				document.getElementById("show-pagination-reviews").innerHTML = getPaginationReviews(res.TotalRecords);
			}
		}
	});
});
