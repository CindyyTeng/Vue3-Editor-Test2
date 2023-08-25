<template>
  <section id="contact" class="pb-5">
    <ShoesnavBar></ShoesnavBar>
    <h1 class="section-header">Contact</h1>
    <h4 class="section-header2">合作洽詢</h4>
    <div class="contact-wrapper">
      <!-- Left contact page -->

      <form id="contact-form" class="form-horizontal" role="form">
        <div class="form-group">
          <div class="col-sm-12">
            <input
              type="text"
              class="form-control"
              id="name"
              placeholder="廠商名稱"
              name="name"
              value=""
              required
            />
          </div>
        </div>

        <div class="form-group">
          <div class="col-sm-12">
            <input
              type="text"
              class="form-control"
              id="contactman"
              placeholder="聯絡人"
              name="contactman"
              value=""
              required
            />
          </div>
        </div>

        <div class="form-group">
          <div class="col-sm-12">
            <input
              type="tel"
              class="form-control"
              id="phonenumber"
              placeholder="聯絡電話"
              name="phonenumber"
              value=""
              required
              maxlength="10"
            />
          </div>
        </div>

        <div class="form-group">
          <div class="col-sm-12">
            <input
              type="email"
              class="form-control"
              id="email"
              placeholder="聯絡信箱"
              name="email"
              value=""
              required
            />
          </div>
        </div>

        <textarea
          class="form-control"
          rows="10"
          placeholder="請詳細說明您的需求"
          name="message"
          required
        ></textarea>

        <button class="btn send-button" id="submit" type="submit" value="SEND">
          <div class="alt-send-button">
            <i class="fa fa-paper-plane"></i><span class="send-text">SEND</span>
          </div>
        </button>
      </form>

      <!-- Left contact page -->

      <div class="direct-contact-container">
        <ul class="contact-list">
          <li class="list-item">
            <i class="bi bi-geo-alt-fill" style="font-size: 24px"
              ><span class="contact-text place">桃園, 台灣</span>
              <div class="map-container">
                <iframe
                  src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1808.2188644470245!2d121.22088753882664!3d24.985237925662496!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x34682183e7b783c3%3A0xf0ebfba2069b6158!2z6IGW5b635Z-6552j5a246Zmi!5e0!3m2!1szh-TW!2stw!4v1691641559512!5m2!1szh-TW!2stw"
                  width="300"
                  height="225"
                  style="border: 0"
                  allowfullscreen=""
                  loading="lazy"
                  referrerpolicy="no-referrer-when-downgrade"
                ></iframe></div
            ></i>
          </li>

          <li class="list-item">
            <i class="bi bi-telephone-outbound-fill" style="font-size: 24px"
              ><span class="contact-text phone"
                ><a href="tel:0961548620" title="Give me a call"
                  >(886) 961-000-000</a
                ></span
              ></i
            >
          </li>

          <li class="list-item">
            <i class="bi bi-envelope-at-fill" style="font-size: 24px"
              ><span class="contact-text gmail"
                ><a href="mailto:#" title="Send me an email"
                  >as48957210@gmail.com</a
                ></span
              ></i
            >
          </li>
        </ul>

        <hr />
        <ul class="social-media-list">
          <li>
            <a href="#" target="_blank" class="contact-icon">
              <i class="fa-brands fa-twitter" aria-hidden="true"></i
            ></a>
          </li>
          <li>
            <a href="#" target="_blank" class="contact-icon">
              <i class="fa-brands fa-facebook-f" aria-hidden="true"></i
            ></a>
          </li>
          <li>
            <a href="#" target="_blank" class="contact-icon">
              <i class="fa-brands fa-line" aria-hidden="true"></i
            ></a>
          </li>
          <li>
            <a href="#" target="_blank" class="contact-icon">
              <i class="fa-brands fa-instagram" aria-hidden="true"></i
            ></a>
          </li>
        </ul>
        <hr />

        <div class="copyright">&copy; ALL OF THE RIGHTS RESERVED</div>
      </div>
    </div>
  </section>
  <homeFooter></homeFooter>
</template>
<script setup>
  import ShoesnavBar from "@/components/customeShoes/ShoesnavBar.vue";
  
  document.addEventListener('DOMContentLoaded', function() {
  const phoneNumberInput = document.getElementById('phonenumber');

  phoneNumberInput.addEventListener('input', function (event) {
    const input = event.target.value;
    const numericInput = input.replace(/\D/g, ''); // Remove non-numeric characters

    if (numericInput.length > 10) {
      phoneNumberInput.value = numericInput.slice(0, 10); // Truncate to 10 digits
    } else {
      phoneNumberInput.value = numericInput;
    }
  });
});
//import homeFooter from "@/components/home/footer.vue";
</script>
<style>
body {
  margin: 0;
  padding: 0;
  padding-bottom: 100px;
}

#contact {
  width: 100%;
  height: 100%;
}

.section-header {
  text-align: center;
  margin: 0 auto;
  padding-top: 40px;
  font: 300 60px 'Oswald', sans-serif;
  color: black;
  text-transform: uppercase;
  letter-spacing: 6px;
}

.section-header2 {
  text-align: center;
  font: 150 30px 'Oswald', sans-serif;
  color: black;
  text-transform: uppercase;
  letter-spacing: 6px;
}

.contact-wrapper {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  margin: 0 auto;
  padding: 20px;
  position: relative;
  max-width: 840px;
}

/* Left contact page */
.form-horizontal {
  /*float: left;*/
  max-width: 400px;
  font-family: 'Lato';
  font-weight: 400;
}

.form-control,
textarea {
  max-width: 400px;
  margin-top: 10px;
  color: #000;
  letter-spacing: 1px;
}

.form-control,
textarea::placeholder {
  color: #bbb;
  opacity: 0.7;
}

input[type='text']::placeholder {
  color: #bbb;
  opacity: 0.8;
}

input[type='tel']::placeholder {
  color: #bbb;
  opacity: 0.8;
}

input[type='email']::placeholder {
  color: #bbb;
  opacity: 0.8;
}

.send-button {
  margin-top: 15px;
  height: 34px;
  width: 400px;
  overflow: hidden;
  background-color: lightblue;
  transition: all 0.2s ease-in-out;
}

.fa-paper-plane {
  color: #fff;
}

.alt-send-button {
  width: 400px;
  height: 34px;
  transition: all 0.2s ease-in-out;
}

.send-text {
  display: block;
  margin-top: 10px;
  font: 700 12px 'Lato', sans-serif;
  letter-spacing: 2px;
  color: black;
}

.alt-send-button:hover {
  transform: translate3d(0px, -29px, 0px);
}

/* Begin Right Contact Page */
.direct-contact-container {
  max-width: 400px;
}

/* Location, Phone, Email Section */
.contact-list {
  list-style-type: none;
  margin-left: -30px;
  padding-right: 20px;
}

.list-item {
  line-height: 4;
  color: #aaa;
}

.fa-twitter,
.fa-facebook-f,
.fa-line,
.fa-instagram {
  color: #fff;
}

.social-media-list li:hover .fa-brands {
  color: black;
  text-decoration: none;
}

.contact-text {
  font: 300 18px 'Lato', sans-serif;
  letter-spacing: 1.9px;
  color: #bbb;
}

.place {
  margin-left: 62px;
}

.phone {
  margin-left: 56px;
}

.gmail {
  margin-left: 53px;
}

.contact-text a {
  color: #bbb;
  text-decoration: none;
  transition-duration: 0.2s;
}

.contact-text a:hover {
  color: black;
  text-decoration: none;
}

/* Social Media Icons */
.social-media-list {
  position: relative;
  font-size: 22px;
  text-align: center;
  width: 100%;
  margin: 0 auto;
  padding: 0;
}

.social-media-list li a {
  color: #fff;
}

.social-media-list li {
  position: relative;
  display: inline-block;
  height: 60px;
  width: 60px;
  margin: 10px 3px;
  line-height: 60px;
  border-radius: 50%;
  color: #fff;
  background-color: lightblue;
  cursor: pointer;
  transition: all 0.2s ease-in-out;
}

.social-media-list li:after {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 60px;
  height: 60px;
  line-height: 60px;
  border-radius: 50%;
  opacity: 0;
  box-shadow: 0 0 0 1px #fff;
  transition: all 0.2s ease-in-out;
}

.social-media-list li:hover {
  background-color: #fff;
}

.social-media-list li:hover:after {
  opacity: 1;
  transform: scale(1.12);
  transition-timing-function: cubic-bezier(0.37, 0.74, 0.15, 1.65);
}

.social-media-list li:hover a {
  color: #000;
}

.copyright {
  font: 200 14px 'Oswald', sans-serif;
  color: #555;
  letter-spacing: 1px;
  text-align: center;
}

hr {
  border-color: rgba(255, 255, 255, 0.6);
}

/* Begin Media Queries*/
@media screen and (max-width: 850px) {
  .contact-wrapper {
    display: flex;
    flex-direction: column;
  }
  .direct-contact-container,
  .form-horizontal {
    margin: 0 auto;
  }

  .direct-contact-container {
    margin-top: 60px;
    max-width: 300px;
  }
  .social-media-list li {
    height: 60px;
    width: 60px;
    line-height: 60px;
  }
  .social-media-list li:after {
    width: 60px;
    height: 60px;
    line-height: 60px;
  }
}

@media screen and (max-width: 569px) {
  .direct-contact-container,
  .form-wrapper {
    float: none;
    margin: 0 auto;
  }
  .form-control,
  textarea {
    margin: 0 auto;
  }

  .name,
  .email,
  textarea {
    width: 280px;
  }

  .direct-contact-container {
    margin-top: 60px;
    max-width: 280px;
  }
  .social-media-list {
    left: 0;
  }
  .social-media-list li {
    height: 55px;
    width: 55px;
    line-height: 55px;
    font-size: 2rem;
  }
  .social-media-list li:after {
    width: 55px;
    height: 55px;
    line-height: 55px;
  }
}

@media screen and (max-width: 410px) {
  .send-button {
    width: 99%;
  }
}
</style>
