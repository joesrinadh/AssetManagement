import React from "react"
import { IAsset } from '../../common'
import { RouteComponentProps, withRouter } from "react-router"
import { connect } from 'react-redux';
import { createStyles, withStyles, WithStyles } from '@material-ui/core/styles';
import { Button, ButtonBase, Grid, Paper, Typography } from "@material-ui/core";
import CardMedia from '@material-ui/core/CardMedia';
import { CustomizedDialog } from '../common/Dailog'
import { ApplicationState } from "../../store";
import * as AssetStore from '../../store/Asset';
import { UploadAsset } from "../upload/UploadAsset";
import { AssetType } from "../../common/Asset";

const useStyles = createStyles({
    root: {
        flexGrow: 1,
    },
    paper: {
        margin: 20,
        maxWidth: 500,
    },
    image: {
        width: 128,
        height: 128,
    },
    img: {
        margin: 'auto',
        display: 'block',
        maxWidth: '100%',
        maxHeight: '100%',
    },
    media: {
        margin: 'auto',
        display: 'block',
        maxWidth: '100%',
        maxHeight: '100%',
    }
});

interface IAssetState {
    isOpenDailog: boolean
    assetName: string
    metadata: string
}

interface Styles extends WithStyles<typeof useStyles> { }
type AssetProps = RouteComponentProps<{ assetId: string }> & Styles &
    AssetStore.AssetState & typeof AssetStore.actionCreators;

class AssetC extends React.Component<AssetProps, IAssetState> {
    public constructor(props: AssetProps) {
        super(props);
        this.state = {
            isOpenDailog: false,
            assetName: '',
            metadata: ''
        }
    }

    public componentDidMount() {
        this.loadAssetDetails();
    }

    public componentDidUpdate() {
        this.loadAssetDetails();
    }

    public render() {
        const { classes } = this.props
        return (
            <>
                <div className={classes.root}>
                    <div>Asset Details</div>
                    {this.props.isLoading && <span>Loading</span>}
                    <Paper className={classes.paper}>
                        <Grid container spacing={2}>
                            <Grid item>
                                <UploadAsset assetId={this.props.match.params.assetId} refreshData={this.loadAssetDetails.bind(this, true)} />
                            </Grid>
                        </Grid>
                    </Paper>
                    {!this.props.isLoading && this.props.assets.map((asset) => (
                        <Paper className={classes.paper} key={asset.AssetId}>
                            <Grid container spacing={2}>
                                <Grid item>
                                    <ButtonBase className={classes.image}>
                                        {asset.AssetType === AssetType.Image &&
                                            <img className={classes.img} alt="complex" src={asset.AssetPath} />
                                        }
                                        {asset.AssetType === AssetType.Video &&
                                            <CardMedia
                                                className={classes.media}
                                                component="video"
                                                src={asset.AssetPath}
                                                title={asset.AssetName}
                                                autoPlay
                                            />
                                        }
                                    </ButtonBase>
                                </Grid>
                                <Grid item xs={12} sm container>
                                    <Grid item xs container direction="column" spacing={2}>
                                        <Grid item xs>
                                            <Typography gutterBottom variant="subtitle1">
                                                {asset.AssetName}
                                            </Typography>
                                            <Typography variant="body2" gutterBottom>
                                                <Button
                                                    variant="contained"
                                                    color="default"
                                                    onClick={this.openDailog.bind(this, asset)}
                                                >
                                                    View Metadata
                                            </Button>
                                            </Typography>
                                        </Grid>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </Paper>
                    ))}
                    {this.state.isOpenDailog &&
                        <CustomizedDialog isOpen={this.state.isOpenDailog} assetName={this.state.assetName} metadata={this.state.metadata} handleClose={this.onDailogclose.bind(this)} />
                    }
                </div>
            </>
        )
    }

    private openDailog(asset: IAsset) {
        this.setState({ isOpenDailog: true, assetName: asset.AssetName, metadata: asset.Metadata })
    }

    private onDailogclose(isOpen: boolean) {
        this.setState({ isOpenDailog: isOpen })
    }

    private loadAssetDetails(isForceRefresh?: boolean) {
        this.props.requestAssets(this.props.match.params.assetId, isForceRefresh);
    }
}

export const Asset = withRouter(connect(
    (state: ApplicationState) => state.asset,
    AssetStore.actionCreators
)(withStyles(useStyles)(AssetC as any)))