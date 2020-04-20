import { Vue, Component, Prop, Watch } from 'vue-property-decorator'
import { UserViewModel } from '../model';
import { UrlPathEnum } from '../../Share/Enums';
import moment = require('moment');
import service from '../service';

@Component({
    template: '#UserEditManagement'
})

export default class UserEditManagement extends Vue {

    httpURL: string = window.location.href;

    image: string = '';

    PhotoFile: File | null = null;
    DefaultImage: string = '';
    ImageName: string = '';

    ur_id: string = '';

    ur_ac: string = '';
    ur_pw: string = '';
    ConfirmPassword: string = '';

    PasswordTypes: string = "password";
    ShowPassword: boolean = false;

    Select: string = '';

    ListItem: UserViewModel | null = null;

    SaveForm: string = 'Loading';
    Message: string = '';

    created() {
        const _this = this;
        _this.ur_id = _this.httpURL.split("?ur_id=")[1];
        _this.GetUserItem(_this.ur_id);
    }

    GetUserItem(ur_id) {
        const _this = this;
        service.GetEditUserItem(ur_id).then(res => {
            if (!res.Success) {
                console.log(res);
            }
            if (res.Data) {
                _this.ListItem = res.Data;
                _this.ur_ac = res.Data.ur_ac;
            }
        }).catch(err => {
            console.log(err);
        })
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

    SetEditUser() {
        const _this = this;
        _this.$bvModal.show('UserModel');
        _this.SaveForm = 'Loading';

        const {
            PhotoFile,
            ur_id,
            ur_ac,
            ur_pw,
        } = this;

        const _formdata = new FormData();
        _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '')
        _formdata.append('ur_id', ur_id)
        _formdata.append('ur_ac', ur_ac)
        _formdata.append('ur_pw', ur_pw)

        _this.EditUserItem(_formdata);
    }

    EditUserItem(model) {
        const _this = this;

        if (_this.ListItem) {

            service.EditUserItem(model).then(res => {
                if (!res.Success) {
                    _this.SaveForm = 'Error';
                    _this.Message = res.Message;
                    console.log(res);
                } if (res.Success) {
                    _this.Message = res.Message;
                    _this.SaveForm = 'Success';
                }
            }).catch(err => {
                _this.SaveForm = 'Error';
                console.log(err);
            })
        }
    }

    get VisibleStopAuthority() {
        const ur_ac = this.ur_ac,
            ur_pw = this.ur_pw,
            ConfirmPassword = this.ConfirmPassword

        return Boolean(ur_ac && ur_pw && ConfirmPassword && ur_pw == ConfirmPassword);
    }

    HideModal() {
        const _this = this;
        _this.$bvModal.hide('UserModel');
    }

    CloseModal() {
        const _this = this;
        _this.$bvModal.hide('UserModel');
        const locationURL = this.httpURL.split("/Edit?")[0];
        document.location.href = locationURL;
    }

    @Watch('ListItem')
    GetDefaultImgUrl() {
        const _this = this;
        const BasePath = window.BasePath; // _Layout.cshtml
        if (_this.ListItem) {
            const photo = _this.ListItem.ur_im;
            _this.DefaultImage = BasePath + UrlPathEnum.UserPhoto + '?filename=' + photo;
        }
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