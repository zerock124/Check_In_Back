import { Vue, Component, Prop, Watch } from 'vue-property-decorator'
import { UserViewModel, RegisterViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import moment = require('moment');
import service from '../service';

@Component({
    template: '#UserCreateManagement'
})

export default class UserCreateManagement extends Vue {
    httpURL: string = window.location.href;

    image: string = '';

    PhotoFile: File | null = null;
    DefaultImage: string = '';
    ImageName: string = '';

    ur_ac: string = '';
    ur_pw: string = '';
    ConfirmPassword: string = '';

    PasswordTypes: string = "password";
    ShowPassword: boolean = false;

    SaveForm: string = 'Loading';
    Message: string = '';

    created() {
        const _this = this;
        _this.GetDefaultImgUrl();
    }

    ChangePasswordType() {
        const _this = this;
        if (_this.PasswordTypes == "password") {
            _this.PasswordTypes = "text";
            _this.ShowPassword = true;
        }
        else {
            _this.PasswordTypes = "password";
            _this.ShowPassword = false;
        }
    }

    get VisibleStopAuthority() {
        const ur_ac = this.ur_ac,
            ur_pw = this.ur_pw,
            ConfirmPassword = this.ConfirmPassword

        return Boolean(ur_ac && ur_pw && ConfirmPassword && ur_pw == ConfirmPassword);
    }

    SetCreateUser() {
        const _this = this;
        _this.$bvModal.show('UserModel');
        _this.SaveForm = 'Loading';

        const {
            PhotoFile,
            ur_ac,
            ur_pw,
            ConfirmPassword,
        } = this;

        const _formdata = new FormData();
        _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '')
        _formdata.append('ur_ac', ur_ac)
        _formdata.append('ur_pw', ur_pw)

        _this.CreateUser(_formdata);
    }

    CreateUser(data) {
        const _this = this;
        console.log(data);
        service.CreateUser(data).then(res => {
            if (!res.Success) {
                _this.SaveForm = 'Error';
                _this.Message = res.Message;
                console.log(res);
            }
            if (res.Success) {
                _this.Message = res.Message;
                _this.SaveForm = 'Success';

            }
        }).catch(err => {
            _this.SaveForm = 'Error';
            console.log(err);
        })
    }

    HideModal() {
        const _this = this;
        _this.$bvModal.hide('UserModel');
    }

    CloseModal() {
        const _this = this;
        _this.$bvModal.hide('UserModel');
        const locationURL = this.httpURL.split("/Create")[0];
        document.location.href = locationURL;
    }

    GetDefaultImgUrl() {
        const _this = this;
        const BasePath = window.BasePath; // _Layout.cshtml
        const photo = "NoImage.jpg";
        _this.DefaultImage = BasePath + UrlPathEnum.UserPhoto + '?filename=' + photo;
    };

    fileSelected(event) {
        const file = event.target.files.item(0); //取得File物件
        this.ImageName = file.name;
        const reader = new FileReader(); //建立FileReader 監聽 Load 事件
        reader.addEventListener('load', this.imageLoader);
        reader.readAsDataURL(file);
    };

    imageLoader(event) {
        this.image = event.target.result;
    }

}
