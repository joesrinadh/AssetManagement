import React from "react"
import { RouteComponentProps, withRouter } from "react-router"
import { connect } from 'react-redux';
import { createStyles, withStyles, WithStyles } from '@material-ui/core/styles';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
import CardMedia from '@material-ui/core/CardMedia';
import ListSubheader from '@material-ui/core/ListSubheader';
import IconButton from '@material-ui/core/IconButton';
import InfoIcon from '@material-ui/icons/Info';
import { ApplicationState } from "../../store";
import * as AssetsStore from '../../store/Assets';
import { UploadAsset } from "../upload/UploadAsset";
import { AssetType } from "../../common/Asset";

const useStyles = createStyles({
    root: {
        display: 'flex'
    },
    gridList: {
        width: 500,
        height: '100%',
    },
    uploadGridList: {
        padding: 150
    },
    icon: {
        color: 'rgba(255, 255, 255, 0.54)',
    },
    media: {
        height: 140,
    }
});
interface Styles extends WithStyles<typeof useStyles> { }
type AssetProps = RouteComponentProps<{}> &
    AssetsStore.AssetsState & typeof AssetsStore.actionCreators & Styles;

class AssetsC extends React.Component<AssetProps> {
    public constructor(props: AssetProps) {
        super(props)
        this.onViewAssetDetailsViewClick = this.onViewAssetDetailsViewClick.bind(this);
    }

    public componentDidMount() {
        this.refreshData();
    }

    public render() {
        const { classes } = this.props
        return (
            <div className={classes.root}>
                <GridList cellHeight={180} className={classes.gridList} >
                    <GridListTile key="Subheader" cols={2} style={{ height: 'auto' }}>
                        <ListSubheader component="div">Assets</ListSubheader>
                        {this.props.isLoading && <span>Loading</span>}
                    </GridListTile>
                    {!this.props.isLoading && this.props.assets.map((asset, index) => (
                        <GridListTile key={asset.AssetId + index}>
                            {asset.AssetType === AssetType.Image &&
                                <img src={asset.AssetPath} alt={asset.AssetName} />
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
                            <GridListTileBar
                                title={asset.AssetName}
                                actionIcon={
                                    <IconButton aria-label={`info about ${asset.AssetName}`} title={`View details of ${asset.AssetName}`} className={classes.icon} onClick={this.onViewAssetDetailsViewClick.bind(this, asset.AssetId)}>
                                        <InfoIcon />
                                    </IconButton>
                                }
                            />
                        </GridListTile>
                    ))}
                </GridList>
                <GridList cellHeight={180} className={classes.uploadGridList} >
                    <GridListTile key="Subheader" cols={2} style={{ height: 'auto' }}>
                        <UploadAsset refreshData={this.refreshData.bind(this)} />
                    </GridListTile>
                </GridList>
            </div>
        )
    }

    private onViewAssetDetailsViewClick(assetId: string) {
        console.log(assetId)
        this.props.history.push(`/asset/${assetId}`);
    }

    private refreshData(): void {
        this.props.requestAssets();
    }
}

export const Assets = withRouter(connect(
    (state: ApplicationState) => state.assets,
    AssetsStore.actionCreators
)(withStyles(useStyles)(AssetsC as any)))