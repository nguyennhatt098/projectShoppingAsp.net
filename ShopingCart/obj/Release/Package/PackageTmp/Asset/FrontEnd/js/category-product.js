﻿var getUrlParameter = function getUrlParameter(sParam) {
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
var categoryId = currentUrl.split('-')[currentUrl.split('-').length - 1];
var url = getUrlParameter('page')
	? '/Product/GetProduct?id=' + categoryId + '&page=' + getUrlParameter('page')
	: '/Product/GetProduct?id=' + categoryId;
$.ajax({
	url: url,
	type: 'GET',
	success: (res) => {
		if (res.items) {
			document.querySelector("#productHot").innerHTML = this.getData(res);
			document.getElementById("paginator").innerHTML = this.getPagination(res);
		}
	}
});


function getCookie(name) {
	var value = document.cookie;
	var parts = value.split("; " + name + "=");
	if (parts.length == 2) return parts[1];
}

function getData(res) {
	var data = res.items;
	var userId = this.getCookie('UserID');
	var txt = '';
	for (i in data) {
		var checkWishList = userId == undefined
			? false
			: getWishListByUserProduct(data[i].wishLists, userId, data[i].Id);
		var url = /chi-tiet-san-pham/ + data[i].Slug + '-' + data[i].Id;
		var urlCart = "/them-vao-gio-hang?productID=" + data[i].Id + "&quantity=1";
		txt += "<div class='col-md-4 col-sm-6'>";
		txt += "<div style='height: 460px;' class='product-wrapper mb-40'>";
		txt += "<div class='product-img'>";
		txt += "<a href=" +
			url +
			"><img  style='width: 100%'  src=" +
			data[i].Images +
			" alt=" +
			data[i].Name +
			" /></a>";
		txt += data[i].TopHot ? "<span class='new-label-hot'>Hot</span>" : "<span class='new-label'>New</span>";
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
	if (data.length == 0) {
		txt += "<p>Danh mục không có sản phẩm</p>";
	}
	document.querySelector(".heading-counter").innerHTML = "Hiển thị " + data.length + " sản phẩm";
	return txt;
}

function getWishListByUserProduct(wishLists, userId, productId) {
	for (var i = 0; i < wishLists.length; i++) {
		var obj = wishLists[i];
		var arr = obj.UserID == userId && obj.ProductID == productId;
		if (arr) return true;
	}
	return false;
}

function getPagination(res) {
	var txt = '';
	var data = res.items;
	var pageIndex = getUrlParameter('page') ? getUrlParameter('page') : 1;
	var totalPage = Math.ceil(res.TotalRecords / 6);

	var start = (pageIndex <= 3 || totalPage == 0) ? 1 : pageIndex - 3;
	var end = (totalPage <= pageIndex + 3) ? totalPage : pageIndex + 3;
	if (end >= totalPage) {
		end = totalPage;
	}
	txt += "<div class='product-count display-inline'>Showing 1 - " +
		data.length +
		" of " +
		res.TotalRecords +
		" items</div> ";
	txt += "<ul class='shop-pagi display-inline'>";

	if (res.TotalRecords) {
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
	let currentUrl = window.location.pathname;
	let categoryId = currentUrl.split('-')[currentUrl.split('-').length - 1];
	$('html, body').animate({
		scrollTop: $("#productHot").offset().top - 200
	}, 1000);
	var urlNavigate = '';
	var pageIndex = getUrlParameter('page') ? getUrlParameter('page') : 1;
	if ($(this).text() == '') {
		if ($(this).attr("events") == 'back') {
			pageIndex -= 1;
			urlNavigate = '/Product/GetProduct?id=' + categoryId + '&page=' + pageIndex;
		} else {
			pageIndex = +pageIndex + 1;
			urlNavigate = '/Product/GetProduct?id=' + categoryId + '&page=' + pageIndex;
		}
	} else {
		pageIndex = $(this).text();
		urlNavigate = '/Product/GetProduct?id=' + categoryId + '&page=' + $(this).text();
	}
	$.ajax({
		url: urlNavigate,
		type: 'GET',
		success: (res) => {
			if (res.items) {
				document.querySelector("#productHot").innerHTML = getData(res);
				window.history.pushState('Danh mục sản phẩm', 'Title', currentUrl + '?page=' + pageIndex);
				document.getElementById("paginator").innerHTML = getPagination(res);
			}
		}
	});
});
$(document).on("click", ".category", function () {
	let url = $(this).attr("url");
	$.ajax({
		url: url,
		type: 'GET',
		success: (res) => {

			if (res.items) {
				window.history.pushState('Danh mục sản phẩm', 'Title', $(this).attr('urlHome'));
				document.querySelector("#productHot").innerHTML = getData(res);
				document.getElementById("paginator").innerHTML = getPagination(res);
				GetCategorybyId();

			}
		}
	});
});
function GetCategorybyId() {

	let currentUrl = window.location.pathname;
	let categoryId = currentUrl.split('-')[currentUrl.split('-').length - 1];
	$.ajax({
		url: '/Product/GetCategoryById?id=' + categoryId,
		type: 'GET',
		success: (item) => {

			if (item) {
				document.querySelector(".category-name").innerHTML = item.Name;
				document.querySelector(".cat-name").innerHTML = item.Name;
			}
		}
	});
}
GetCategorybyId();