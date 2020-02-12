$.ajax({
	url: '/Home/GetProductNew',
	type: 'GET',
	success: (res) => {
		if (res) {
			document.querySelector("#productNew").innerHTML = this.getData(res, true);
			$("#productNew").owlCarousel({
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
$.ajax({
	url: '/Home/GetProductHot',
	type: 'GET',
	success: (res) => {
		if (res) {
			document.querySelector("#productHot").innerHTML = this.getData(res, false);
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
$.ajax({
	url: '/Home/GetBlog',
	type: 'GET',
	success: (res) => {
		if (res) {
			var txt = '';
			var data = JSON.parse(res);
			for (i in data) {
				var url = /chi-tiet-tin-tuc/ + data[i].Slug + '-' + data[i].ID;
				var date = new Date(data[i].CreateDate);
				var day = date.getDate();
				var monthIndex = date.getMonth() + 1;
				var year = date.getFullYear();
				txt += "<div class='col-lg-12'>";
				txt += "<div class='blog-wrapper mb-40'>";
				txt += "<div class='blog-img'>";
				txt += "<a href=" + url + "><img  src=" + data[i].Images + " alt=" + data[i].Name + " /></a>";
				txt += "</div>";
				txt += "<div class='blog-info'>";
				txt += "<h3><a href=" + url + ">" + data[i].Summary + "</a></h3>";
				txt += "<div class='blog-meta'>";
				txt += "<span class='f-left'>" + day + "/" + monthIndex + "/" + year + "</span>";
				txt += "<span class='f-right'><a href=" + url + ">Xem Thêm </a></span>";
				txt += "</div>";
				txt += "</div>";
				txt += "</div>";
				txt += "</div>";
			}
			document.querySelector("#blog-list").innerHTML = txt;
			$('#blog-list').owlCarousel({
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
						items: 2
					}
				}
			})
		}
	}
});
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
function getData(data, isNew) {
	var userId = this.getCookie('UserID');
	var txt = '';
	for (i in data) {
		var checkWishList = userId == undefined ? false : getWishListByUserProduct(data[i].wishLists, userId, data[i].Id);
		var url = /chi-tiet-san-pham/ + data[i].Slug + '-' + data[i].Id;
		var urlCart = "/them-vao-gio-hang?productID=" + data[i].Id + "&quantity=1";
		txt += "<div class='col-md-12'>";
		txt += "<div class='product-wrapper mb-40'>";
		txt += "<div class='product-img'>";
		txt += "<a href=" + url + "><img  src=" + data[i].Images + " alt=" + data[i].Name + " /></a>";
		txt += isNew ? "<span class='new-label'>New</span>" : "<span class='new-label-hot'>Hot</span>";
		txt += "<div class='product-action'>";
		txt += "<a href=" + urlCart + "><i class='pe-7s-cart' ></i></a>";
		if (userId == undefined) {
			txt += "<a data-toggle='modal' data-target='#exampleModalCenter' ><i class='fa fa-heart'></i></a>";
		}
		else if (checkWishList) {
			txt += "<a class='wishlist wishlistDisLike wishlist-" + data[i].Id + "' id=" + data[i].Id + " data-id=" + data[i].Id + " href='javascript:void(0)'><i class='fa fa-heart'></i></a>";
		}
		else {
			txt += "<a class='wishlist wishlist-" + data[i].Id + "' id=" + data[i].Id + " data-id=" + data[i].Id + " href='javascript:void(0)'><i class='fa fa-heart'></i></a>";
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
			txt += "<span class='old-price product-price'>" + nf.format(data[i].Price) + "VND</span> <span class='price product-price'>" + nf.format(data[i].Sale_Price) + "VND</span>";
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
				}
				else if (data[i].AverageStar % 1 !== 0 && check) {
					check = false;
					txt += "<i class='fa fa-star-half-o' aria-hidden='true'></i>";
				}
				else {
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

$(document).on("click", ".category", function () {
	window.location.href = $(this).attr('urlHome');
});
