document.addEventListener("DOMContentLoaded", function () {
  const contextMenu = document.getElementById('contextMenu');
  const deleteModal = $('#modal-sm');

  const closeMenu = function (event) {
    if (!contextMenu.contains(event.target)) {
      contextMenu.style.display = 'none';
      document.removeEventListener('click', closeMenu);
    }
  };

  document.querySelectorAll('.context-menu-row').forEach(function (row) {
    row.addEventListener('contextmenu', function (e) {
      e.preventDefault();
      console.log("Right-clicked row");

      const rowId = row.getAttribute('data-id');
      const controllerName = row.getAttribute('data-controller');

      const viewportWidth = window.innerWidth;
      const viewportHeight = window.innerHeight;
      const menuWidth = contextMenu.offsetWidth;
      const menuHeight = contextMenu.offsetHeight;
      let posX = e.pageX;
      let posY = e.pageY;

      if (posX + menuWidth > viewportWidth) posX = viewportWidth - menuWidth - 10;
      if (posY + menuHeight > viewportHeight) posY = viewportHeight - menuHeight - 10;

      contextMenu.style.top = `${posY}px`;
      contextMenu.style.left = `${posX}px`;
      contextMenu.style.display = 'block';

      document.getElementById('editOption').onclick = function () {
        console.log("Edit option clicked");
        const editUrl = `/${controllerName}/Edit?id=${rowId}`;
        window.location.href = editUrl;
      };

      document.getElementById('infoOption').onclick = function () {
        console.log("Info option clicked");
        const infoUrl = `/${controllerName}/Info?id=${rowId}`;
        window.location.href = infoUrl;
      }

      document.getElementById('deleteOption').onclick = function () {
        console.log("Delete option clicked");
        deleteModal.attr('data-row-id', rowId);
        deleteModal.attr('data-controller-name', controllerName);
        deleteModal.modal('show');
      };

      };

      document.addEventListener('click', closeMenu);
    });
  });
});
