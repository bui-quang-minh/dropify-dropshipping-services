"use strict";

var con = new signalR.HubConnectionBuilder().withUrl("/hub").build();

con.on("ReloadNews", function (newsList) {
    processNewsList(newsList);
});



con.start().then().catch(function (err) {
    return console.error(err.toString());
});
function processNewsList(newsList) {
    var notificationList = document.querySelector('.notification-list');

    // Xóa tất cả các tin tức hiện có trong danh sách để làm sạch trước khi thêm tin tức mới
    notificationList.innerHTML = '';

    // Duyệt qua mỗi tin tức trong danh sách và thêm chúng vào danh sách HTML
    newsList.forEach(function (news) {
        var listItem = document.createElement('li');
        listItem.classList.add('notification-message');

        var link = document.createElement('a');
        link.href = '#';

        var mediaDiv = document.createElement('div');
        mediaDiv.classList.add('media', 'd-flex');

        var avatarSpan = document.createElement('span');
        avatarSpan.classList.add('avatar', 'avatar-sm', 'flex-shrink-0');

        var avatarImg = document.createElement('img');
        avatarImg.classList.add('avatar-img', 'rounded-circle');
        avatarImg.alt = 'User Image';
        avatarImg.src = '~/Admin/img/user/user.jpg'; // Thay đổi đường dẫn ảnh theo thông tin tin tức nếu cần

        avatarSpan.appendChild(avatarImg);

        var mediaBodyDiv = document.createElement('div');
        mediaBodyDiv.classList.add('media-body', 'flex-grow-1');

        var detailsPara = document.createElement('p');
        detailsPara.classList.add('noti-details');
        detailsPara.innerHTML = '<span class="noti-title">' + news.author + '</span> ' + news.title;

        var timePara = document.createElement('p');
        timePara.classList.add('noti-time');
        timePara.innerHTML = '<span class="notification-time">' + news.timestamp + '</span>';

        mediaBodyDiv.appendChild(detailsPara);
        mediaBodyDiv.appendChild(timePara);

        mediaDiv.appendChild(avatarSpan);
        mediaDiv.appendChild(mediaBodyDiv);

        link.appendChild(mediaDiv);
        listItem.appendChild(link);

        notificationList.appendChild(listItem);
    });
}