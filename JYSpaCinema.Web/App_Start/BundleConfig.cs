using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace JYSpaCinema.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/modernizr").Include(
                "~/Content/vendors/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/vendors").Include(
                //jquery 2.1.2
                "~/Content/vendors/jquery/jquery.js",
                //bootstrap 3.3.6
                "~/Content/vendors/bootstrap/js/bootstrap.js",
                //toastr
                "~/Content/vendors/toastr/toastr.js",
                //underscore
                "~/Content/vendors/underscore/underscore.js",
                //respond(快速、轻量的 polyfill，用于为 IE6-8 以及其它不支持 CSS3 Media Queries 的浏览器提供媒体查询的 min-width 和 max-width 特性)
                "~/Content/vendors/respond/respond.src.js",
                //angularjs 1.5.3
                "~/Content/vendors/angular/angular.js",
                "~/Content/vendors/angular-route/angular-route.js",
                "~/Content/vendors/angular-cookies/angular-cookies.js",
                //"~/Content/vendors/angular-validator/angular-validator.js",
                "~/Content/vendors/angular-base64/angular-base64.js",
                "~/Content/vendors/angular-file-upload/angular-file-upload.js",
                "~/Content/vendors/angucomplete-alt/angucomplete-alt.min.js",
                //angular-ui-bootstrap
                "~/Content/vendors/angular-bootstrap/ui-bootstrap-tpls.js",
                //jquery.raty
                "~/Content/vendors/jquery-raty/jquery.raty.js",
                //raphael(矢量图库)
                "~/Content/vendors/raphael/raphael.js",
                //morris(时间序列图库)
                "~/Content/vendors/morris/morris.js",
                //jquery.fancybox
                "~/Content/vendors/fancybox/jquery.fancybox.js",
                "~/Content/vendors/fancybox/jquery.fancybox-media.js",
                //angularjs loading-bar
                "~/Content/vendors/angular-loading-bar/loading-bar.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/css/vendors").Include(
                //bootstrap 3.3.6
                "~/Content/vendors/bootstrap/css/bootstrap.css",
                "~/Content/vendors/bootstrap/css/bootstrap-theme.css",
                //font-awesome
                "~/Content/vendors/font-awesome/css/font-awesome.css",
                //toastr
                "~/Content/vendors/toastr/toastr.css",
                //morris(时间序列图库)
                "~/Content/vendors/morris/morris.css",
                //jquery.fancybox
                "~/Content/vendors/fancybox/jquery.fancybox.css",
                //angularjs loading-bar
                "~/Content/vendors/angular-loading-bar/loading-bar.css",
                //site custom
                "~/Content/css/site.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/scripts/spa").Include(
                "~/Scripts/spa/modules/common.core.js",
                "~/Scripts/spa/modules/common.ui.js",
                "~/Scripts/spa/app.js",
                //service
                "~/Scripts/spa/services/notificationService.js",
                "~/Scripts/spa/services/apiService.js",
                "~/Scripts/spa/services/membershipService.js",
                //layout
                "~/Scripts/spa/layout/sideBar.directive.js",
                "~/Scripts/spa/layout/topBar.directive.js",
                //directive
                "~/Scripts/spa/directives/customPager.directive.js",
                //controller
                "~/Scripts/spa/home/rootCtrl.js",
                "~/Scripts/spa/home/indexCtrl.js",
                "~/Scripts/spa/account/loginCtrl.js",
                "~/Scripts/spa/account/registerCtrl.js",
                 "~/Scripts/spa/customers/customersCtrl.js",
                 "~/Scripts/spa/customers/customersRegCtrl.js",
                 "~/Scripts/spa/customers/customerEditCtrl.js"
                ));

            //BundleTable.EnableOptimizations = false;
        }
    }
}