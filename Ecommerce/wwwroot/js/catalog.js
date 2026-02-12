(function () {
  async function addToCart(bookId) {
    try {
      var response = await fetch("/Cart/AddItem?bookId=" + encodeURIComponent(bookId), {
        headers: {
          "X-Requested-With": "XMLHttpRequest"
        }
      });

      if (response.redirected) {
        window.location.href = "/Identity/Account/Login";
        return;
      }

      if (!response.ok) {
        return;
      }

      var contentType = response.headers.get("content-type") || "";
      if (!contentType.includes("application/json")) {
        return;
      }

      var result = await response.json();
      var cartCountEl = document.getElementById("cartCount");
      if (cartCountEl) {
        cartCountEl.innerText = String(result);
      }
    } catch {
      // Keep UX stable on network/server errors.
    }
  }

  window.catalogActions = {
    addToCart: addToCart
  };
})();
