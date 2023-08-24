"use strict";

// Class definition
var KTProjectsAdd = function () {
	// Base elements
	var wizardEl;
	var formEl;
	var validator;
	var wizard;
	var avatar;

	// Private functions
	var initWizard = function () {
		// Initialize form wizard
		wizard = new KTWizard('kt_projects_add', {
			startStep: 1, // initial active step number
			clickableSteps: true  // allow step clicking
		});

		// Validation before going to next page
		wizard.on('beforeNext', function(wizardObj) {
			if (validator.form() !== true) {
				wizardObj.stop();  // don't go to the next step
			}
		})

		// Change event
		wizard.on('change', function(wizard) {
			KTUtil.scrollTop();
		});
	}

    var initValidation = function () {
        validator = formEl.validate({
            // Validate only visible fields
            ignore: ":hidden",

            // Validation rules
            rules: {
                // Step 1
                ProgrammeCode: {
                    required: true
                },
                Session: {
                    required: true
                },
                Semester: {
                    required: true
                },
                Level: {
                    required: true
                },
                coursecode: {
                    required: true
                }
                ,
                agree: {
                    required: true
                }

               

                //coursecode
            },

            // Display error
            invalidHandler: function (event, validator) {
                KTUtil.scrollTop();

                swal.fire({
                    "title": "",
                    "text": "There are some errors in your submission. Please correct them.",
                    "type": "error",
                    "buttonStyling": false,
                    "confirmButtonClass": "btn btn-brand btn-sm btn-bold"
                });
            },

            // Submit valid form
            submitHandler: function (form) {
              
            }
        });
    }

	var initSubmit = function() {
		var btn = formEl.find('[data-ktwizard-type="action-submit"]');

		btn.on('click', function(e) {
			e.preventDefault();

			if (validator.form()) {
				// See: src\js\framework\base\app.js

                // check confirmation

				KTApp.progress(btn);
				//KTApp.block(formEl);

				// See: http://malsup.com/jquery/form/#ajaxSubmit
				formEl.ajaxSubmit({
                    success: function (responseText, statusText, xhr, $form) {
                        KTApp.unprogress(btn);
                        //KTApp.unblock(formEl);
                       // alert(responseText);
                        if (responseText.status === "Succeed") {
                            swal.fire({
                                "title": "",
                                "text": "The create curriculum operation was successfully submitted!",
                                "type": "success",
                                "confirmButtonClass": "btn btn-secondary"
                            });
                        } else {
                            swal.fire({
                                "title": "",
                                "text": "The create curriculum operation failed with error details : " + responseText.message,
                                "type": "error",
                                "confirmButtonClass": "btn btn-secondary"
                            });
                        }
						
                    },
                    dataType: 'json'
				});
			}
		});
	}

	var initAvatar = function() {
		avatar = new KTAvatar('kt_projects_add_avatar');
	}

	return {
		// public functions
		init: function() {
			formEl = $('#kt_projects_add_form');

			initWizard();
			initValidation();
			initSubmit();
			initAvatar();
		}
	};
}();

jQuery(document).ready(function() {
	KTProjectsAdd.init();
});
