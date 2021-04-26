import React from "react";
import { connect } from "react-redux";
import { RouteComponentProps, withRouter } from "react-router";
import { Typography, Button } from '@material-ui/core';
import UploadService from '../../services/UploadFile.service'

interface IUploadAsset {
  selectedFiles: any,
  message: string,
  isError: boolean
};

type UploadAssetProps = { assetId?: string, refreshData(): void } & RouteComponentProps<{}>;

class UploadAssetC extends React.Component<UploadAssetProps, IUploadAsset> {
  public constructor(props: UploadAssetProps) {
    super(props);
    this.state = {
      selectedFiles: undefined,
      message: "",
      isError: false
    };
  }

  public render() {
    const {
      selectedFiles,
      message,
      isError
    } = this.state;
    return (
      <div className="mg20">
        <label htmlFor="btn-upload">
          <input
            id="btn-upload"
            name="btn-upload"
            style={{ display: 'none' }}
            type="file"
            onChange={this.selectFile.bind(this)} />
          <Button
            className="btn-choose"
            variant="outlined"
            component="span" >
            Choose Files
          </Button>
        </label>
        <div className="file-name">
          {selectedFiles && selectedFiles.length > 0 ? selectedFiles[0].name : null}
        </div>
        <Button
          className="btn-upload"
          color="primary"
          variant="contained"
          component="span"
          disabled={!selectedFiles}
          onClick={this.upload.bind(this)}>
          Upload
        </Button>

        <Typography variant="subtitle2" className={`upload-message ${isError ? "error" : ""}`}>
          {message}
        </Typography>
      </div >
    );
  }

  selectFile(event: any) {
    this.setState({
      selectedFiles: event.target.files,
    });
  }

  upload() {
    let currentFile = this.state.selectedFiles[0];
    UploadService.upload(currentFile, this.props.assetId).then((response) => {
      if (response && response.status == 200) {
        this.setState({
          message: 'file uploaded successfully',
          isError: false
        });
        this.props.refreshData();
      } else {
        this.setState({
          message: "Could not upload the file!",
          isError: true
        });
      }
    }, (error) => {
      this.setState({
        message: "Could not upload the file!",
        isError: true
      });
    }).catch(() => {
      this.setState({
        message: "Could not upload the file!",
        isError: true
      });
    });
    this.setState({
      selectedFiles: undefined,
    });
  }
}

export const UploadAsset = withRouter(connect()(UploadAssetC))