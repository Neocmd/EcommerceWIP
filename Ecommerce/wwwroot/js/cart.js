(function () {
  async function loadCartCount() {
    var cartCountEl = document.getElementById("cartCount");
    if (!cartCountEl) {
      return;
    }

    try {
      var response = await fetch("/Cart/GetTotalItemInCart", {
        headers: {
          "X-Requested-With": "XMLHttpRequest"
        }
      });

      if (!response.ok || response.redirected) {
        return;
      }

      var contentType = response.headers.get("content-type") || "";
      if (!contentType.includes("application/json")) {
        return;
      }

      var result = await response.json();
      cartCountEl.innerText = String(result);
    } catch {
      // Keep UI stable when cart count cannot be loaded.
    }
  }

  window.ecommerceCart = {
    loadCartCount: loadCartCount
  };

  document.addEventListener("DOMContentLoaded", function () {
    loadCartCount();
  });
})();
