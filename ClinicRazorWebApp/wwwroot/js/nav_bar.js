//const navItems = document.querySelectorAll('.navItem');
//const activeContainer = document.querySelector('.navItemActiveContainer');
//const positions = [570, 370, 170, -30];

//// Hàm lưu trạng thái đã click vào local storage
//function saveActiveIndex(index) {
//    localStorage.setItem('activeNavIndex', index.toString());
//}

//// Hàm đọc trạng thái từ local storage và cập nhật giao diện
//function loadActiveIndex() {
//    const activeIndex = localStorage.getItem('activeNavIndex');
//    if (activeIndex !== null) {
//        updateActiveState(parseInt(activeIndex));
//    }
//}

//function updateActiveState(index) {
//    if (activeContainer) {
//        activeContainer.style.right = positions[index] + 'px';
//        saveActiveIndex(index); // Lưu trạng thái vào local storage khi update
//    }
//}

//// Thực hiện load trạng thái khi trang được tải
//document.addEventListener('DOMContentLoaded', () => {
//    loadActiveIndex();
//});

//navItems.forEach((item, index) => {
//    item.addEventListener('click', (event) => {
//        event.preventDefault(); // Ngăn không reload trang
//        updateActiveState(index);
//        window.location.href = item.href; // Chuyển hướng đến trang được chọn
//    });
//});

const navItems = document.querySelectorAll('.navItem');
const activeContainer = document.querySelector('.navItemActiveContainer');
const positions = [570, 370, 170, -30];

function saveActiveIndex(index) {
    localStorage.setItem('activeNavIndex', index.toString());
}

function loadActiveIndex() {
    const activeIndex = localStorage.getItem('activeNavIndex');
    if (activeIndex !== null) {
        updateActiveState(parseInt(activeIndex));
    }
}

function updateActiveState(index) {
    if (activeContainer) {
        activeContainer.style.right = positions[index] + 'px';
        saveActiveIndex(index);
    }
}

document.addEventListener('DOMContentLoaded', () => {
    loadActiveIndex();
});

navItems.forEach((item, index) => {
    item.addEventListener('click', (event) => {
        event.preventDefault();
        updateActiveState(index);
        window.location.href = item.href;
    });
});
